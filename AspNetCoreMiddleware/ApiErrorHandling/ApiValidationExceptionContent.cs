using System.Collections.Generic;

namespace AspNetCoreMiddleware.ApiErrorHandling
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