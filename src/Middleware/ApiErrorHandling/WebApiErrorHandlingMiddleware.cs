using System;
using System.Net;
using System.Threading.Tasks;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling;
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
        public const HttpStatusCode ConflictHttpStatusCode = HttpStatusCode.Conflict;
        public const string ConflictReasonPhrase = "Conflict";

        public const HttpStatusCode BadRequestHttpStatusCode = HttpStatusCode.BadRequest;
        public const string BadRequestReasonPhrase = "Bad Request";
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
            catch (ArgumentException exception) when (_config.AllArgumentsException)
            {
                await HandleArgumentException(exception, context);
            }
          
            catch (Exception exception)
            {
                switch (exception)
                {
                    case ArgumentNullException argumentNullException when _config.ArgumentNullException:
                        await HandleArgumentException(argumentNullException, context);
                        break;
                    case ArgumentNullException argumentNullException:
                        throw;
                    case ArgumentOutOfRangeException argumentOutOfRangeException when _config.ArgumentOutOfRangeException:
                        await HandleArgumentException(argumentOutOfRangeException, context);
                        break;
                    case ArgumentOutOfRangeException argumentOutOfRangeException:
                        throw;
                    case ArgumentException argumentException when _config.ArgumentException:
                        await HandleArgumentException(argumentException, context);
                        break;
                    case ArgumentException argumentException:
                        throw;
                    default:
                    {
                        if (exception.TryGetWebApiConflictAttribute(out var apiAttribute))
                        {
                            if (apiAttribute.Message.IsNullOrEmptyOrWhiteSpace())
                            {
                                apiAttribute.Message = exception.Message;
                            }

                            var content = BuildContent(apiAttribute);


                            await SetResponseAndLogContent(_logger,
                                                           context.Response,
                                                           ConflictHttpStatusCode,
                                                           ConflictReasonPhrase,
                                                           content);
                        }
                        else if (exception.TryGetWebApiBadRequestAttribute(out var apiValidationExceptionAttribute))
                        {
                            if (apiValidationExceptionAttribute.Message.IsNullOrEmptyOrWhiteSpace())
                            {
                                apiValidationExceptionAttribute.Message = exception.Message;
                            }


                            var content = BuildContent(apiValidationExceptionAttribute);

                            await SetResponseAndLogContent(_logger,
                                                           context.Response,
                                                           BadRequestHttpStatusCode,
                                                           BadRequestReasonPhrase,
                                                           content);
                        }
                        else
                        {
                            throw;
                        }

                        break;
                    }
                }
            }
        }

        private async Task HandleArgumentException(ArgumentException exception, HttpContext context)
        {
            var index = exception.Message.LastIndexOf("\r\n",
                                                      StringComparison.Ordinal);
            var message = exception.Message.Substring(0,
                                                      index);

            object content = new BadRequestObjectResult(CreateModelState(exception.ParamName,
                                                                         message));
            await SetResponseAndLogContent(_logger,
                                           context.Response,
                                           BadRequestHttpStatusCode,
                                           BadRequestReasonPhrase,
                                           content);
        }

        private static object BuildContent(WebApiConflictAttribute apiAttribute)
        {
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

            return content;
        }

        private static object BuildContent(WebApiBadRequestAttribute apiValidationExceptionAttribute)
        {
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

            return content;
        }


        private static ModelStateDictionary CreateModelState(string fieldName, string message)
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName,
                                     message);

            return modelState;
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