using System;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling
{
    public class WebApiErrorHandlingConflictException : Exception
    {
        public WebApiErrorHandlingConflictException(string message, int? errorCode = null) : base(message)
        {
            ErrorCode = errorCode;
        }


        public int? ErrorCode { get; set; }
    }
}