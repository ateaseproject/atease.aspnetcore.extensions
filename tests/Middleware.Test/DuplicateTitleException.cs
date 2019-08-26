using System;
using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    [ApiValidationException("Title", "DuplicatedWithTitle")]
    public class DuplicateTitleExceptionWithMessage : Exception
    {
    }


    [ApiValidationException("Title")]
    public class DuplicateTitleException : Exception
    {
        public DuplicateTitleException() : base("DuplicatedWithTitle")
        {
        }
    }
}