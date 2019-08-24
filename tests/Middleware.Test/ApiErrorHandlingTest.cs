using System.IO;
using System.Net;
using System.Threading.Tasks;
using AtEase.AspNetCore.Extensions.Middleware;
using AtEase.Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Middleware.Test
{
    public class ApiErrorHandlingTest
    {
        public ApiErrorHandlingTest()
        {
            _logger = new FakeLogger();
        }

        private readonly ILogger _logger;

        [Fact]
        public async Task BeValidExceptionType_ApiException_ConflictStatus()
        {
            var middleware =
                new ApiErrorHandlingMiddleware(innerHttpContext => throw new EntityNotFoundException(), _logger);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = streamText.To<ApiExceptionContent>();

            context.Response.StatusCode
                .Should()
                .Be((int) HttpStatusCode.Conflict);

            objResponse.UiMessage
                .Should()
                .Be("EntityNotFound");

            objResponse.ErrorCode
                .Should()
                .Be(-1);
        }

        [Fact]
        public async Task BeValidExceptionType_ApiValidationException_BadRequestStatus()
        {
            var middleware =
                new ApiErrorHandlingMiddleware(innerHttpContext => throw new DuplicateTitleException(), _logger);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = streamText.To<ApiValidationExceptionContent>();

            context.Response.StatusCode
                .Should()
                .Be((int)HttpStatusCode.BadRequest);

            objResponse.ModelState["Title"][0]
                .Should()
                .Be("DuplicatedWithTitle");
        }
    }
}