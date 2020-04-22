using MicroRabbit.Domain.Core.Events;

namespace Deploy_O_Mat.Service.Domain.Events
{
    public class DockerServiceUpdatedEvent : Event
    {
        public string Image { get; set; }
        public string Service { get; set; }

        public DockerServiceUpdatedEvent(
            string image,
            string service)
        {
            Service = service;
            Image = image;
        }
    }
}
