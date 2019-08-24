using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    public class EntityNotFoundException : ApiException
    {
        public EntityNotFoundException(string displayMessage, int errorCode):base(displayMessage, errorCode)
        {
        }
    }
}