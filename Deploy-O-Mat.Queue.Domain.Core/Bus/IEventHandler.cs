using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Bus
{
    public interface IEventHandler<in TEvent>  : IEventHandler where TEvent: Event
    {
        Task Handle(TEvent @event);
    }
    public interface IEventHandler { }
}
