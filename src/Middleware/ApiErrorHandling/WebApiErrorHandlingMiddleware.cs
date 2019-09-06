using System;
using System.Net;
using System.Threading.Tasks;
using AtEase.Extensions;
using AtEase.Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public static class WebApiErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseWebApiErrorHandling(this IApplicationBuilder builder, ILogger logger)
        {
            return builder.UseMiddleware<WebApiErrorHandlingMiddleware>(logger);
        }
    }


    public class WebApiErrorHandlingMiddleware
    {
        public const HttpStatusCode ConflictHttpStatusCode = HttpStatusCode.Conflict;
        public const string ConflictReasonPhrase = "Conflict";

        public const HttpStatusCode BadRequestHttpStatusCode = HttpStatusCode.BadRequest;
        public const string BadRequestReasonPhrase = "Bad Request";


        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public WebApiErrorHandlingMiddleware(RequestDelegate next, ILogger logger)
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
                if (exception.TryGetWebApiConflictAttribute(out var apiAttribute))
                {
                    if (apiAttribute.Message.IsNullOrEmptyOrWhiteSpace())
                    {
                        apiAttribute.Message = exception.Message;
                    }

                    object content;

                    if (apiAttribute.ErrorCode.IsNotNull())
                    {
                        content = new ConflictObjectResult(CreateModelState(apiAttribute.ErrorCode.Value.ToString(),
                            apiAttribute.Message));
                    }
                    else if (apiAttribute.Message.IsNotNullOrEmpty())
                    {
                        content = new ConflictObjectResult(apiAttribute.Message);
                    }
                    else
                    {
                        content = new ConflictResult();
                    }


                    await SetResponseAndLogContent(_logger, context.Response, ConflictHttpStatusCode,
                        ConflictReasonPhrase, content);
                }
                else if (exception.TryGetWebApiBadRequestAttribute(out var apiValidationExceptionAttribute))
                {
                    if (apiValidationExceptionAttribute.Message.IsNullOrEmptyOrWhiteSpace())
                    {
                        apiValidationExceptionAttribute.Message = exception.Message;
                    }


                    object content;

                    if (apiValidationExceptionAttribute.FieldName.IsNotNullOrEmpty())
                    {
                        content = new BadRequestObjectResult(CreateModelState(apiValidationExceptionAttribute.FieldName,
                            apiValidationExceptionAttribute.Message));
                    }
                    else if (apiValidationExceptionAttribute.Message.IsNotNullOrEmpty())
                    {
                        content = new BadRequestObjectResult(apiValidationExceptionAttribute.Message);
                    }
                    else
                    {
                        content = new BadRequestResult();
                    }

                    await SetResponseAndLogContent(_logger, context.Response, BadRequestHttpStatusCode,
                        BadRequestReasonPhrase, content);
                }
                else
                {
                    throw;
                }
            }
        }


        private static ModelStateDictionary CreateModelState(string fieldName, string message)
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName, message);

            return modelState;
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