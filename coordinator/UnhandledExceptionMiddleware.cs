using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace coordinator
{
    public class UnhandledExceptionMiddleware
    {
        private readonly ILogger<UnhandledExceptionMiddleware> logger;

        public UnhandledExceptionMiddleware(ILogger<UnhandledExceptionMiddleware> logger)
        {
            this.logger = logger;
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                return next(context);
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Uncaught exception in {context.Request.Path}: {e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}