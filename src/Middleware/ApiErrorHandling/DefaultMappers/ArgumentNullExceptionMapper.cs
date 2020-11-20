using System;
using AtEase.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling.DefaultMappers
{
    public class ArgumentNullExceptionMapper : ArgumentExceptionMapper
    {
        public override bool CanHandle(Exception exception)
        {
            return exception.GetType() == typeof(ArgumentNullException);
        }

        public override object CreateContent(Exception exception)
        {
            var ex = (ArgumentNullException) exception;

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