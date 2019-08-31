using System;
using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    [ApiValidationException("Name", "NameValidationException")]
    public class NameValidationExceptionWithMessage : Exception
    {
    }


    [ApiValidationException("Name")]
    public class NameValidationException : Exception
    {
        public NameValidationException() : base("NameValidationException")
        {
        }
    }
}