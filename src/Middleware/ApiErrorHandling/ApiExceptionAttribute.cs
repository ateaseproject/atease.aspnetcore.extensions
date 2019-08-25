using System;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiExceptionAttribute : Attribute
    {
        public ApiExceptionAttribute(string displayMessage = "", int errorCode = 0)
        {
            DisplayMessage = displayMessage;
            ErrorCode = errorCode;
        }

        public string ReferenceCode { get; set; }
        public int ErrorCode { get; protected set; }
        public string DisplayMessage { get; protected set; }
    }
}