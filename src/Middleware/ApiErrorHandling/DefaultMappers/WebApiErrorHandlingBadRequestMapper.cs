using System.Net;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling.DefaultMappers
{
    public abstract class WebApiErrorHandlingBadRequestMapper : WebApiErrorHandlingMapper
    {
        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }

        public override string GetReasonPhrase()
        {
            return "Bad Request";
        }
    }
}