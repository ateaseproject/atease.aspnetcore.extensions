using System.Collections.Generic;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public class ApiValidationExceptionContent
    {
        public ApiValidationExceptionContent()
        {
        }

        public ApiValidationExceptionContent(string traceId, string title, string fieldName, string message)
        {
            Errors = CreateModelState(fieldName, message);
            Title = title;
            TraceId = traceId;
        }


        public Dictionary<string, string[]> Errors { get; set; }
        public string Title { get; set; }
        public int Status => 400;
        public string TraceId { get; set; }

        private static Dictionary<string, string[]> CreateModelState(string fieldName, string message)
        {
            var modelState = new Dictionary<string, string[]>();
            modelState.Add(fieldName, new[] {message});
            return modelState;
        }
    }
}