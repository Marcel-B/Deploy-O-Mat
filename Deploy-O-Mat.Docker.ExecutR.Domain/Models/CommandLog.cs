using System;

namespace com.b_velop.Deploy_O_Mat.Docker.ExecutR.Domain.Models
{
    public class CommandLog
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Caller { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public int ResultCode { get; set; }
    }
}