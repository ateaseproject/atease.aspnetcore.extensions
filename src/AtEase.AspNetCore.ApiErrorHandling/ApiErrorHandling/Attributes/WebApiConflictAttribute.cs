using System;

namespace AtEase.AspNetCore.ApiErrorHandling.ApiErrorHandling.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebApiConflictAttribute : Attribute
    {
        public WebApiConflictAttribute(int errorCode, string message) : this()
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public WebApiConflictAttribute(string message) : this()
        {
            Message = message;
        }

        public WebApiConflictAttribute(int errorCode) : this()
        {
            ErrorCode = errorCode;
        }

        public WebApiConflictAttribute()
        {
        }


        public int? ErrorCode { get; protected set; }
        public string Message { get; set; }
    }
}