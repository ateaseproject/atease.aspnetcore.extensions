using System.Net;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling.DefaultMappers
{
    public abstract class WebApiErrorHandlingConflictMapper : WebApiErrorHandlingMapper
    {
        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.Conflict;
        }

        public override string GetReasonPhrase()
        {
            return "Conflict";
        }
    }
}