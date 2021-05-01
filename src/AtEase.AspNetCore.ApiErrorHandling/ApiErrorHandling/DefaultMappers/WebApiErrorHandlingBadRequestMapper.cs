using System.Net;

namespace AtEase.AspNetCore.ApiErrorHandling.ApiErrorHandling
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