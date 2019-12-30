using System;
using System.IO;
using System.Threading.Tasks;
using AtEase.AspNetCore.Extensions.Middleware;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Middleware.Test
{
    public class WebApiErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task when_exception_without_webapi_attribute_raised_it_should_rethrow()
        {
            var conflictController = new ConflictController();

            var result = conflictController.GetConflict();

            var middleware =
                new WebApiErrorHandlingMiddleware(innerHttpContext => throw new ArgumentException(),
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();


            async Task Act()
            {
                await middleware.Invoke(context);
            }


            await Assert.ThrowsAsync<ArgumentException>(Act);
        }
    }
}