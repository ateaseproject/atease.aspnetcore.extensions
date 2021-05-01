using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AtEase.AspNetCore.ApiErrorHandling
{
    public static class GlobalErrorHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalErrorHandlingMiddleware(this IApplicationBuilder builder,
            ILogger logger)
        {
            return builder.UseMiddleware<GlobalErrorHandlingMiddleware>(logger);
        }
    }

    public class GlobalErrorHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError("Unexpected error caught in {0}:  {1}", nameof(GlobalErrorHandlingMiddleware), e);
                throw;
            }
        }
    }
}