namespace AtEase.AspNetCore.Extensions.Middleware
{
    public class ApiExceptionContent
    {
        public ApiExceptionContent()
        {
        }
        public ApiExceptionContent(ApiException apiException)
        {
            ErrorCode = apiException.ErrorCode;
            Message = apiException.Message;
            DisplayMessage = apiException.DisplayMessage;
        }

        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string DisplayMessage { get; set; }
    }
}