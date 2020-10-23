namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling
{
    public class WebApiErrorHandlingConfig
    {
        public bool ArgumentNullException { get; set; }

        public bool AllArgumentsException { get; set; }

        public bool ArgumentOutOfRangeException { get; set; }
        public bool ArgumentException { get; set; }
    }
}