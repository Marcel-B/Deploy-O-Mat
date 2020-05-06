using System.Net;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Interfaces
{
    public interface IServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
