using System;

namespace com.b_velop.Deploy_O_Mat.Web.Domain.Models
{
    public class RequestLog
    {
        public RequestLog()
        {
            Created = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string ContentType { get; set; }
        public string Header { get; set; }
        public string Path { get; set; }
        public string Query { get; set; }
        public string RemoteIp { get; set; }
        public int RemotePort { get; set; }
        public string Protocol { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public int ResponseStatusCode { get; set; }
        public string PathBase { get; set; }
        public bool IsHttps { get; set; }
        public long Duration { get; set; }
    }
}
