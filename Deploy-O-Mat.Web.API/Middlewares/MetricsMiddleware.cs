using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Prometheus;

namespace com.b_velop.Deploy_O_Mat.Web.API.Middlewares
{
    public class MetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MetricsMiddleware> _logger;

        public MetricsMiddleware(RequestDelegate next, ILogger<MetricsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (Metrics.CreateHistogram("deployomat_request_duration_seconds",
                    "Histogram of login call processing durations.", labelNames: new string[]
                    {
                        "method",
                        "protocol",
                        "path",
                        "code"
                    }).WithLabels(httpContext.Request.Method, httpContext.Request.Protocol, httpContext.Request.Path, httpContext.Response.StatusCode.ToString())
                .NewTimer())
            {
                await _next(httpContext);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MetricsMiddlewareExtensions
    {
        public static IApplicationBuilder UseMetricsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MetricsMiddleware>();
        }
    }
}