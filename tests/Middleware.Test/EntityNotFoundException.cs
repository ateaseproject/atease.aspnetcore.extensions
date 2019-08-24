using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    public class EntityNotFoundException : ApiException
    {
        public EntityNotFoundException():base("EntityNotFound", -1)
        {
        }
    }
}