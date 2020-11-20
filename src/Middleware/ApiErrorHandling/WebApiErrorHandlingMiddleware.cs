using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling.DefaultMappers;
using AtEase.Extensions;
using AtEase.Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public static class WebApiErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebApiErrorHandling(this IApplicationBuilder builder,
                                                                 ILogger logger,
                                                                 WebApiErrorHandlingConfig config)
        {
            return builder.UseMiddleware<WebApiErrorHandlingMiddleware>(logger,
                                                                        config);
        }

        public static IApplicationBuilder UseWebApiErrorHandling(this IApplicationBuilder builder, ILogger logger)
        {
            var config = new WebApiErrorHandlingConfig();
            return builder.UseMiddleware<WebApiErrorHandlingMiddleware>(logger,
                                                                        config);
        }
    }


    public class WebApiErrorHandlingMiddleware
    {
        private readonly WebApiErrorHandlingConfig _config;
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public WebApiErrorHandlingMiddleware(RequestDelegate next, ILogger logger, WebApiErrorHandlingConfig config)
        {
            _next = next;
            _logger = logger;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception exception)
            {
                var exceptionHandled = false;
                if (_config.Mappers.Any())
                {
                    foreach (var mapper in _config.Mappers)
                    {
                        exceptionHandled = mapper.CanHandle(exception);
                        if (exceptionHandled)
                        {
                            await SetResponseAndLogContent(_logger,
                                                           context.Response,
                                                           mapper.GetStatusCode(),
                                                           mapper.GetReasonPhrase(),
                                                           mapper.CreateContent(exception));
                        }
                    }
                }

                if (exceptionHandled)
                {
                    return;
                }


                if (exception.TryGetWebApiConflictAttribute(out var apiAttribute))
                {
                    if (apiAttribute.Message.IsNullOrEmptyOrWhiteSpace())
                    {
                        apiAttribute.Message = exception.Message;
                    }

                    await TryHandle(context,
                                    new WebApiErrorHandlingConflictExceptionMapper(),
                                    new WebApiErrorHandlingConflictException(
                                    apiAttribute.Message,
                                    apiAttribute.ErrorCode));
                }
                else if (exception.TryGetWebApiBadRequestAttribute(out var apiValidationExceptionAttribute))
                {
                    if (apiValidationExceptionAttribute.Message.IsNullOrEmptyOrWhiteSpace())
                    {
                        apiValidationExceptionAttribute.Message = exception.Message;
                    }

                    await TryHandle(context,
                                    new ArgumentExceptionMapper(),
                                    new ArgumentException(apiValidationExceptionAttribute.Message,
                                                          apiValidationExceptionAttribute.FieldName));
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task TryHandle(HttpContext context, WebApiErrorHandlingMapper mapper, Exception exception)
        {
            if (mapper.CanHandle(exception))
            {
                await SetResponseAndLogContent(_logger,
                                               context.Response,
                                               mapper.GetStatusCode(),
                                               mapper.GetReasonPhrase(),
                                               mapper.CreateContent(exception));
            }
            else
            {
                throw new Exception("cannot handle the exception", exception);
            }
        }

        public static Task SetResponseAndLogContent(ILogger logger,
                                                    HttpResponse httpResponse,
                                                    HttpStatusCode statusCode,
                                                    string reasonPhrase,
                                                    object content)
        {
            var result = content.ToJson();
            logger.LogDebug(result);

            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = (int) statusCode;
            httpResponse.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = reasonPhrase;

            return httpResponse.WriteAsync(result);
        }
    }
}