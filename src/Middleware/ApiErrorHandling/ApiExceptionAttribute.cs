using System;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiExceptionAttribute : Attribute
    {
        public ApiExceptionAttribute(int errorCode, string message)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public ApiExceptionAttribute(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public string ReferenceCode { get; set; }
        public int ErrorCode { get; protected set; }
        public string Message { get; set; }
    }
}