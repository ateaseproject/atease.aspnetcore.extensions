using System;
using System.Collections.Generic;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ApiValidationExceptionAttribute : Attribute
    {
        public ApiValidationExceptionAttribute(Dictionary<string, string[]> modelState)
        {
            ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));
        }

        public ApiValidationExceptionAttribute(string fieldName, string error)
        {
            var modelState = new Dictionary<string, string[]>();
            modelState.Add(fieldName, new[] {error});

            ModelState = modelState;
        }

        public Dictionary<string, string[]> ModelState { get; protected set; }
    }
}