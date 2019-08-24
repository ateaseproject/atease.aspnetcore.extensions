using System.Collections.Generic;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public class ApiValidationExceptionContent
    {
        public ApiValidationExceptionContent()
        {
        }
        public ApiValidationExceptionContent(ApiValidationException apiValidationException)
        {
            ModelState = apiValidationException.ModelState;
        }

        public Dictionary<string, string[]> ModelState { get; set; }
    }
}