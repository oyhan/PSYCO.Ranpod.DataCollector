using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSYCO.Ranpod.LocalProxy.Helper
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
            //_logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Request.EnableRewind();

            try
            {
                var request = context.Request;
                //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
                var buffer = new byte[Convert.ToInt32(request.ContentLength)];

                //...Then we copy the entire request stream into the new buffer.
                await request.Body.ReadAsync(buffer, 0, buffer.Length);

                //We convert the byte[] into a string using UTF8 encoding...
                var bodyAsText = Encoding.UTF8.GetString(buffer);
                Log.Logger

                .Information(
                    "Request {method} {url} body: {bodyAsText} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    bodyAsText,
                    context.Response?.StatusCode);
                context.Request.Body.Position = 0;
                await _next(context);
            }
            finally
            {
               
            }
        }
    }
}
