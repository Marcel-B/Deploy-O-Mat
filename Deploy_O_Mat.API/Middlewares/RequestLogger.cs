using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Domain;
using com.b_velop.Deploy_O_Mat.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.Deploy_O_Mat.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestLogger
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public RequestLogger(
            RequestDelegate next,
            IServiceProvider serviceProvider
            )
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task Invoke(
            HttpContext httpContext)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            httpContext.Request.EnableBuffering();
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            var requestLog = new RequestLog
            {
                ContentType = httpContext.Request.ContentType,
                Path = httpContext.Request.Path,
                Query = httpContext.Request.QueryString.Value,
                RemoteIp = httpContext.Connection.RemoteIpAddress.ToString(),
                RemotePort = httpContext.Connection.RemotePort,
                Protocol = httpContext.Request.Protocol,
                Method = httpContext.Request.Method,
                PathBase = httpContext.Request.PathBase,
                IsHttps = httpContext.Request.IsHttps
            };

            var header = httpContext.Request.Headers.Select(x => $"{x.Key}: { x.Value}");
            requestLog.Header = string.Join(',', header);
            using var sr = new StreamReader(httpContext.Request.Body, encoding: Encoding.UTF8, leaveOpen: true);
            requestLog.Body = await sr.ReadToEndAsync();
            httpContext.Request.Body.Position = 0;
            await _next(httpContext);
            requestLog.ResponseStatusCode = httpContext.Response.StatusCode;
            stopWatch.Stop();
            requestLog.Duration = stopWatch.ElapsedMilliseconds;
            context.RequestLogs.Add(requestLog);
            await context.SaveChangesAsync();
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestLoggerExtensions
    {
        public static IApplicationBuilder UseRequestLogger(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogger>();
        }
    }
}
