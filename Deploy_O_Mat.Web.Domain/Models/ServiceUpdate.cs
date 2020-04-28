using System;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class ServiceUpdate
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; }
    }
}