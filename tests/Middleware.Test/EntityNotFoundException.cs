using System;
using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    [WebApiConflict(-1, "EntityNotFound")]
    public class EntityNotFoundExceptionWithMessage : Exception
    {
    }


    [WebApiConflict(-1)]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("EntityNotFound")
        {
        }
    }




    [WebApiConflict()]
    public class EntityWithErrorMessageException : Exception
    {
        public EntityWithErrorMessageException() : base("EntityNotFound")
        {
        }
    }

    [WebApiConflict()]
    public class EntityException : Exception
    {
        public EntityException() : base("")
        {
        }
    }



}