using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Threading.Tasks;

namespace PdvApi.Middleware
{
    public class ExceptionMiddleware
    {
        public RequestDelegate Next { get; }

        public ILogger Logger { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);
            }
            catch (Exception e)
            {
                Logger.Error(e, "An internal error occurred, contact a support frambachluiz@gmail.com");
                throw;
            }
        }
    }
}
