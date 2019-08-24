using System;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling;

namespace Middleware.Test
{
    [ApiException("EntityNotFound", -1)]
    public class EntityNotFoundException : Exception
    {
    }
}