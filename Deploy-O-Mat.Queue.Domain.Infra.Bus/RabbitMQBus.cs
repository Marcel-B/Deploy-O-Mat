using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Commands;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;
using com.b_velop.Utilities.Docker;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace com.b_velop.Deploy_O_Mat.Queue.Infra.Bus
{
    public sealed class RabbitMQBus : IEventBus
    {
        private readonly SecretProvider _secretProvider;
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _eventTypes;
        private readonly string userName = "guest";
        private string passWord = "guest";
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RabbitMQBus(
            IMediator mediator,
            IServiceScopeFactory serviceScopeFactory,
            SecretProvider secretProvider)
        {
            _secretProvider = secretProvider;
            _mediator = mediator;
            _serviceScopeFactory = serviceScopeFactory;
            _handlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
            userName = secretProvider.GetSecret("rabbit_user") ?? "guest";
            passWord = secretProvider.GetSecret("rabbit_pass") ?? "guest";
        }

        public Task SendCommand<T>(T command) where T : Command
            => _mediator.Send(command);

        /// <summary>
        /// Send / Publish (Producer)
        /// </summary>
        /// <param name="event"></param>
        /// <typeparam name="T"></typeparam>
        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory
            {
                HostName = _secretProvider.GetSecret("HOSTNAME") ?? "localhost",
                Port = 5672,
                UserName = userName ?? "guest",
                Password = passWord ?? "guest"
            };
            
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            var eventName = @event.GetType().Name;
            channel.QueueDeclare(eventName, false, false, false, null);
            
            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish("", eventName, null, body);
        }

        public void PublishRpc(
            Guid correlationId, 
            string replyTo)
        {
            var factory = new ConnectionFactory
            {
                HostName = _secretProvider.GetSecret("HOSTNAME") ?? "localhost",
                Port = 5672,
                UserName = userName ?? "guest",
                Password = passWord ?? "guest"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("rpc_queue", false, false, false, null);
            channel.BasicQos(0, 1, false);
            
            var consumer = new EventingBasicConsumer(channel);
            
            channel.BasicConsume("rpc_queue", false, consumer);
            consumer.Received += (model, ea) =>
            {
                string response = null;

                var body = ea.Body;
                var props = ea.BasicProperties;
                var replyProps = channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                try
                {
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    var n = int.Parse(message);
                   // => Method call
                   // response = fib(n).ToString();
                }
                catch (Exception ex)
                {
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish("", props.ReplyTo, basicProperties: replyProps,body: responseBytes);
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };
        }

        public void SubscribeRpc<T, TH>() 
            where T : Event
            where TH : IEventHandler
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for {eventName}", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }
        
        /// <summary>
        /// Subscribe to a Queue
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TH"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler
        {
            var eventName = typeof(T).Name;
            var handlerType = typeof(TH);

            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));

            if (!_handlers.ContainsKey(eventName))
                _handlers.Add(eventName, new List<Type>());

            if (_handlers[eventName].Any(s => s.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler Type {handlerType.Name} already is registered for {eventName}", nameof(handlerType));
            }

            _handlers[eventName].Add(handlerType);

            StartBasicConsume<T>();
        }

        /// <summary>
        /// Consume
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory
            {
                HostName = _secretProvider.GetSecret("HOSTNAME") ?? "localhost",
                DispatchConsumersAsync = true,
                Port = 5672,
                UserName = userName ?? "guest",
                Password = passWord ?? "guest"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            var eventName = typeof(T).Name;

            channel.QueueDeclare(eventName, false, false, false, null);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;

            channel.BasicConsume(eventName, true, consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            var message = Encoding.UTF8.GetString(e.Body.ToArray());

            try
            {
                await ProcessEvent(eventName, message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task ProcessEvent(
            string eventName, 
            string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var subscriptions = _handlers[eventName];
                foreach (var subscription in subscriptions)
                {
                    var handler = scope.ServiceProvider.GetService(subscription);
                    if (handler == null) continue;
                    var eventType = _eventTypes.SingleOrDefault(t => t.Name == eventName);
                    var @event = JsonConvert.DeserializeObject(message, eventType);
                    var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                }
            }
        }
    }
}
