using System.Collections.Generic;
using com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.Models;
using com.b_velop.Deploy_O_Mat.Queue.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Application.Bus.Events
{
    public class UpdateServicesEvent : Event
    {
        public IEnumerable<DockerService> DockerServices { get; set; }
        
        public UpdateServicesEvent(IEnumerable<DockerService> dockerServices)
        {
            DockerServices = dockerServices;
        }
    }
}