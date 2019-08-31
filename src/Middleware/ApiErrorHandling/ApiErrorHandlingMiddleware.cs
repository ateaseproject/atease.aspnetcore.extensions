using System;
using System.Net;
using System.Threading.Tasks;
using AtEase.Extensions;
using AtEase.Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public static class ApiErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiErrorHandling(this IApplicationBuilder builder, ILogger logger)
        {
            return builder.UseMiddleware<ApiErrorHandlingMiddleware>(logger);
        }
    }


    public class ApiErrorHandlingMiddleware
    {
        public const HttpStatusCode ApiExceptionHttpStatusCode = HttpStatusCode.Conflict;
        public const string ApiExceptionReasonPhrase = "Conflict";
        public const string ApiExceptionSwaggerDescription = "Conflict";


        public const HttpStatusCode ApiValidationExceptionHttpStatusCode = HttpStatusCode.BadRequest;
        public const string ApiValidationExceptionReasonPhrase = "Bad Request";
        public const string ApiValidationExceptionSwaggerDescription = "Bad Request";


        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ApiErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                if (exception.TryGetApiExceptionAttribute(out var apiAttribute))
                {
                    if (apiAttribute.Message.IsNullOrEmptyOrWhiteSpace())
                    {
                        apiAttribute.Message = exception.Message;
                    }

                    await SetResponseAndLogContent(_logger, context.Response, ApiExceptionHttpStatusCode,
                        ApiExceptionReasonPhrase,
                        new ApiExceptionContent(apiAttribute));
                }
                else if (exception.TryGetApiValidationExceptionAttribute(out var apiValidationExceptionAttribute))
                {
                    var message = apiValidationExceptionAttribute.Message.IsNotNullOrEmptyOrWhiteSpace()
                        ? apiValidationExceptionAttribute.Message
                        : exception.Message;

                    var content = new ApiValidationExceptionContent(context.TraceIdentifier,
                        apiValidationExceptionAttribute.Title, apiValidationExceptionAttribute.FieldName, message);


                    await SetResponseAndLogContent(_logger, context.Response, ApiValidationExceptionHttpStatusCode,
                        ApiValidationExceptionReasonPhrase, content);
                }
            }
        }

        public static Task SetResponseAndLogContent(ILogger logger, HttpResponse httpResponse,
            HttpStatusCode statusCode, string reasonPhrase, object content)
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