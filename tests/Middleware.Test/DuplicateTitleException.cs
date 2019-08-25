using System;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling;

namespace Middleware.Test
{
    [ApiValidationException("Title", "DuplicatedWithTitle")]
    public class DuplicateTitleException : Exception
    {
    }
}