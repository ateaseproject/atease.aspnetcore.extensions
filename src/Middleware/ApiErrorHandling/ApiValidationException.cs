using System;
using System.Collections.Generic;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public class ApiValidationException : Exception
    {
        public ApiValidationException(Dictionary<string, string[]> modelState) : base()
        {
            ModelState = modelState ?? throw new ArgumentNullException(nameof(modelState));
        }

        public ApiValidationException(string fieldName, string error) : base()
        {
            var modelState = new Dictionary<string, string[]>();
            modelState.Add(fieldName, new[] { error });

            ModelState = modelState;
        }

        public Dictionary<string, string[]> ModelState { get; protected set; }
    }
}
