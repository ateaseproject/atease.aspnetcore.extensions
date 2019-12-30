using System;
using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    [WebApiBadRequest("Name", "NameValidationException")]
    public class NameValidationExceptionWithMessage : Exception
    {
    }


    [WebApiBadRequest("Name")]
    public class NameValidationException : Exception
    {
        public NameValidationException() : base("NameValidationException")
        {
        }
    }

    [WebApiBadRequest]
    public class ValidationExceptionWithErrorMessage : Exception
    {
        public ValidationExceptionWithErrorMessage() : base("NameValidationException")
        {
        }
    }


    [WebApiBadRequest()]
    public class ValidationException : Exception
    {
        public ValidationException() : base("")
        {
        }
    }
}