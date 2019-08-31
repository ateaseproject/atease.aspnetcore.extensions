using System;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiValidationExceptionAttribute : Attribute
    {
        public ApiValidationExceptionAttribute(string fieldName)
        {
            FieldName = fieldName;
        }


        public ApiValidationExceptionAttribute(string fieldName, string message) : this(fieldName)
        {
            Message = message;
        }


        public ApiValidationExceptionAttribute(string title, string fieldName, string message) : this(fieldName,
            message)
        {
            Title = title;
        }

        public string FieldName { get; }
        public string Message { get; }
        public string Title { get; }
    }
}