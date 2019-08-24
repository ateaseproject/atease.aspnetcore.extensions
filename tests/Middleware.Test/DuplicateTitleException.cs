using System.Collections.Generic;
using AtEase.AspNetCore.Extensions.Middleware;

namespace Middleware.Test
{
    public class DuplicateTitleException : ApiValidationException
    {
        public DuplicateTitleException(Dictionary<string, string[]> modelState) : base(modelState)
        {
        }

        public DuplicateTitleException() : base("Title", "DuplicatedWithTitle")
        {
        }
    }
}