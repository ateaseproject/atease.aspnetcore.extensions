namespace AspNetCoreMiddleware.ApiErrorHandling
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
            UiMessage = apiException.UiMessage;
        }

        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string UiMessage { get; set; }
    }
}