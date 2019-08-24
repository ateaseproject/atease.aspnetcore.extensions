using System.Collections.Generic;
using AspNetCoreMiddleware.ApiErrorHandling;

namespace AspNetCoreMiddleware.Test
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