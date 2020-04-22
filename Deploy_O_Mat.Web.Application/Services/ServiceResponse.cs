using System;
using System.Net;
using com.b_velop.Deploy_O_Mat.Web.Application.Interfaces;

namespace com.b_velop.Deploy_O_Mat.Web.Application.Services
{
    public class ServiceResponse : IServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Error { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
