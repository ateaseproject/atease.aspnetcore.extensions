using System;
using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    [ApiException(-1, "EntityNotFound")]
    public class EntityNotFoundExceptionWithMessage : Exception
    {
    }


    [ApiException(-1)]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("EntityNotFound")
        {
        }
    }
}