using System;

namespace AtEase.AspNetCore.ApiErrorHandling.ApiErrorHandling.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebApiBadRequestAttribute : Attribute
    {
        public WebApiBadRequestAttribute(string fieldName, string message) : this(fieldName)
        {
            Message = message;
        }


        public WebApiBadRequestAttribute(string fieldName)
        {
            FieldName = fieldName;
        }


        public WebApiBadRequestAttribute()
        {
        }


        public string FieldName { get; protected set; }
        public string Message { get; set; }
    }
}