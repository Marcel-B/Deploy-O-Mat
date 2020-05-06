using System.Collections.Generic;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Bus.Events
{
    public class UpdateServicesEvent : Event
    {
        public IEnumerable<Models.DockerService> DockerServices { get; set; }
    }
}