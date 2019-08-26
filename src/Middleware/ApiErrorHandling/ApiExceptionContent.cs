namespace AtEase.AspNetCore.Extensions.Middleware
{
    public class ApiExceptionContent
    {
        public ApiExceptionContent()
        {
        }

        public ApiExceptionContent(ApiExceptionAttribute apiException)
        {
            ErrorCode = apiException.ErrorCode;
            Message = apiException.Message;
        }

        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}