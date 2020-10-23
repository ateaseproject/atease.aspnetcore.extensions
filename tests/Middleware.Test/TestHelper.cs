using AtEase.AspNetCore.Extensions.Middleware;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling;
using Microsoft.AspNetCore.Http;

namespace Middleware.Test
{
    public static class TestHelper
    {
        public static WebApiErrorHandlingMiddleware BuildWebApiErrorHandlingMiddleware(
        RequestDelegate next, WebApiErrorHandlingConfig config)
        {
            return new WebApiErrorHandlingMiddleware(next,
                                                     new FakeLogger(),
                                                     config);
        }

        public static WebApiErrorHandlingMiddleware BuildWebApiErrorHandlingMiddleware(
        RequestDelegate next)
        {
            return new WebApiErrorHandlingMiddleware(next,
                                                     new FakeLogger(),
                                                     new WebApiErrorHandlingConfig());
        }
    }
}