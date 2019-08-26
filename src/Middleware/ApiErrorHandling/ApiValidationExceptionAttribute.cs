using System;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiValidationExceptionAttribute : Attribute
    {
        //public ApiValidationExceptionAttribute(Dictionary<string, string[]> modelState)
        //{
        //    ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));
        //}

        public ApiValidationExceptionAttribute(string fieldName, string message)
        {
            FieldName = fieldName;
            Message = message;
        }

        public ApiValidationExceptionAttribute(string fieldName)
        {
            FieldName = fieldName;
        }


        //public Dictionary<string, string[]> ModelState { get; protected set; }
        public string FieldName { get; protected set; }
        public string Message { get; set; }
    }
}