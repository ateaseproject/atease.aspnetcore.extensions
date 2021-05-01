using System;
using AtEase.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AtEase.AspNetCore.ApiErrorHandling.ApiErrorHandling
{
    public class ArgumentOutOfRangeExceptionMapper : WebApiErrorHandlingBadRequestMapper
    {
        public override bool CanHandle(Exception exception)
        {
            return exception.GetType() == typeof(ArgumentOutOfRangeException);
        }

        public override object CreateContent(Exception exception)
        {
            var ex = (ArgumentOutOfRangeException) exception;


            if (ex.ParamName.IsNotNullOrEmptyOrWhiteSpace())
            {
                var index = exception.Message.LastIndexOf("\r\n",
                                                          StringComparison.Ordinal);
                var message = exception.Message.Substring(0,
                                                          index);

                return new BadRequestObjectResult(CreateModelState(ex.ParamName, message));
            }

            if (ex.Message.IsNotNullOrEmptyOrWhiteSpace())
            {
                return new BadRequestObjectResult(ex.Message);
            }

            return new BadRequestResult();
        }
    }
}