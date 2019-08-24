using System;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public abstract class ApiException : Exception
    {
        public ApiException(string displayMessage = "", int errorCode = 0)
        {
            DisplayMessage = displayMessage;
            ErrorCode = errorCode;
        }
        public string ReferenceCode { get; set; }
        public int ErrorCode { get; protected set; }
        public string DisplayMessage { get; protected set; }
    }
}