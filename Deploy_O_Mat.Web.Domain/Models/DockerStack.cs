﻿using System;
namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class DockerStack
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
    }
}