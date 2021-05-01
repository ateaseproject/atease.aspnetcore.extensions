using System;
using AtEase.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AtEase.AspNetCore.ApiErrorHandling.ApiErrorHandling
{
    public class WebApiErrorHandlingConflictExceptionMapper : WebApiErrorHandlingConflictMapper
    {
        public override bool CanHandle(Exception exception)
        {
            return exception.GetType() == typeof(WebApiErrorHandlingConflictException);
        }

        public override object CreateContent(Exception exception)
        {
            var ex = (WebApiErrorHandlingConflictException) exception;

            if (ex.ErrorCode.IsNotNull())
            {
                return new ConflictObjectResult(CreateModelState(ex.ErrorCode.Value.ToString(),
                                                                 ex.Message));
            }

            if (ex.Message.IsNotNullOrEmpty())
            {
                return new ConflictObjectResult(ex.Message);
            }

            return new ConflictResult();
        }
    }
}