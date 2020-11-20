using System;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebApiConflictAttribute : Attribute
    {
       


        public WebApiConflictAttribute(int errorCode, string message) : this(errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
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