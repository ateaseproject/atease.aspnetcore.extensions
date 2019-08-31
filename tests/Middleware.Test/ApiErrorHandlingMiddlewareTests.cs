using System.IO;
using System.Net;
using System.Threading.Tasks;
using AtEase.AspNetCore.Extensions.Middleware;
using AtEase.Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Middleware.Test
{
    public class ApiErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task when_ApiException_raised_it_should_return_Conflict_http_status_code()
        {
            const string displayMessage = "EntityNotFound";
            const int errorCode = -1;

            var middleware =
                new ApiErrorHandlingMiddleware(
                    innerHttpContext => throw new EntityNotFoundException(), new FakeLogger());

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

            objResponse.Message
                .Should()
                .Be(displayMessage);

            objResponse.ErrorCode
                .Should()
                .Be(errorCode);
        }


        [Fact]
        public async Task when_ApiExceptionWithMessage_raised_it_should_return_Conflict_http_status_code()
        {
            const string displayMessage = "EntityNotFound";
            const int errorCode = -1;

            var middleware =
                new ApiErrorHandlingMiddleware(
                    innerHttpContext => throw new EntityNotFoundExceptionWithMessage(), new FakeLogger());

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

            objResponse.Message
                .Should()
                .Be(displayMessage);

            objResponse.ErrorCode
                .Should()
                .Be(errorCode);
        }

        [Fact]
        public async Task when_ApiValidationException_raised_it_should_return_BadRequest_http_status_code()
        {
            const string fieldName = "Title";
            const string error = "DuplicatedWithTitle";

            var middleware =
                new ApiErrorHandlingMiddleware(innerHttpContext => throw new DuplicateTitleException(),
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = streamText.To<ApiValidationExceptionContent>();

            context.Response.StatusCode
                .Should()
                .Be((int) HttpStatusCode.BadRequest);

            objResponse.Errors[fieldName][0]
                .Should()
                .Be(error);
        }


        [Fact]
        public async Task when_ApiValidationExceptionWithMessage_raised_it_should_return_BadRequest_http_status_code()
        {
            const string fieldName = "Title";
            const string error = "DuplicatedWithTitle";

            var middleware =
                new ApiErrorHandlingMiddleware(innerHttpContext => throw new DuplicateTitleExceptionWithMessage(),
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();
            var objResponse = streamText.To<ApiValidationExceptionContent>();

            context.Response.StatusCode
                .Should()
                .Be((int) HttpStatusCode.BadRequest);

            objResponse.Errors[fieldName][0]
                .Should()
                .Be(error);
        }
    }
}