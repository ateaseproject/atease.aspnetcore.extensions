using System.Collections.Generic;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public class ApiValidationExceptionContent
    {
        public ApiValidationExceptionContent()
        {
        }

        public ApiValidationExceptionContent(ApiValidationExceptionAttribute apiValidationException)
        {
            Errors = CreateModelState(apiValidationException.FieldName, apiValidationException.Message);
        }


        public Dictionary<string, string[]> Errors { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }

        private static Dictionary<string, string[]> CreateModelState(string fieldName, string message)
        {
            var modelState = new Dictionary<string, string[]>();
            modelState.Add(fieldName, new[] {message});
            return modelState;
        }
    }
}