using System;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public abstract class ApiException : Exception
    {
        public ApiException(string uiMessage = "", int errorCode = 0)
        {
            UiMessage = uiMessage;
            ErrorCode = errorCode;
        }
        public string ReferenceCode { get; set; }
        public int ErrorCode { get; protected set; }
        public string UiMessage { get; protected set; }
    }
}