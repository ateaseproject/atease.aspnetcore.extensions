using AspNetCoreMiddleware.ApiErrorHandling;

namespace AspNetCoreMiddleware.Test
{
    public class EntityNotFoundException : ApiException
    {
        public EntityNotFoundException():base("EntityNotFound", -1)
        {
        }
    }
}