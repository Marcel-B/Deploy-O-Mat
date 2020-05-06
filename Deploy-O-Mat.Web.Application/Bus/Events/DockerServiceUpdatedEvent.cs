using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events
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