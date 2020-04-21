﻿using MicroRabbit.Domain.Core.Events;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Events
{
    public class DockerStackRemovedEvent : Event
    {
        public string StackName { get; set; }

        public DockerStackRemovedEvent(
            string stackName)
        {
            StackName = stackName;
        }
    }
}