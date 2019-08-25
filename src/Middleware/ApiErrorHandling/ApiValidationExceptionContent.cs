using System.Collections.Generic;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public class ApiValidationExceptionContent
    {
        public ApiValidationExceptionContent()
        {
        }
        public ApiValidationExceptionContent(ApiValidationExceptionAttribute apiValidationException)
        {
            ModelState = apiValidationException.ModelState;
        }

        public Dictionary<string, string[]> ModelState { get; set; }
    }
}