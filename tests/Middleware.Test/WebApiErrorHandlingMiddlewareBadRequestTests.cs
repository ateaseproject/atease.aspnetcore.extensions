using System.IO;
using System.Net;
using System.Threading.Tasks;
using AtEase.AspNetCore.Extensions.Middleware;
using AtEase.Extensions;
using AtEase.Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Middleware.Test
{
    public class WebApiErrorHandlingMiddlewareBadRequestTests
    {
        [Fact]
        public async Task when_WebApiBadRequestException_raised_it_should_return_BadRequest_http_status_code()
        {
            var badRequestController = new BadRequestController();

            var result = badRequestController.GetBadRequest();

            var middleware =
                new WebApiErrorHandlingMiddleware(innerHttpContext => throw new ValidationException(),
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();


            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.BadRequest, context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual, expected));
        }


        [Fact]
        public async Task
            when_WebApiBadRequestException_with_message_raised_it_should_return_BadRequest_http_status_code()
        {
            const string error = "NameValidationException";

            var badRequestController = new BadRequestController();

            var result = badRequestController.GetBadRequestWithError(error);

            var middleware =
                new WebApiErrorHandlingMiddleware(innerHttpContext => throw new ValidationExceptionWithErrorMessage(),
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();


            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.BadRequest, context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual, expected));
        }


        [Fact]
        public async Task
            when_WebApiBadRequestException_with_message_raised_it_should_return_BadRequest_http_status_code2()
        {
            var exception = new ValidationExceptionWithErrorMessage();


            var badRequestController = new BadRequestController();


            var result = badRequestController.GetBadRequestWithError(exception.Message);

            var middleware =
                new WebApiErrorHandlingMiddleware(innerHttpContext => throw exception,
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();


            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.BadRequest, context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual, expected));
        }

        [Fact]
        public async Task
            when_WebApiBadRequestException_with_ModelState_raised_it_should_return_BadRequest_http_status_code()
        {
            const string fieldName = "Name";
            const string error = "NameValidationException";


            var badRequestController = new BadRequestController();

            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName, error);
            var result = badRequestController.GetBadRequestWithModelState(modelState);

            var middleware =
                new WebApiErrorHandlingMiddleware(innerHttpContext => throw new NameValidationExceptionWithMessage(),
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.BadRequest, context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual, expected));
        }

        [Fact]
        public async Task
            when_WebApiBadRequestException_with_ModelState_raised_it_should_return_BadRequest_http_status_code2()
        {
            const string fieldName = "Name";
            const string error = "NameValidationException";


            var badRequestController = new BadRequestController();

            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName, error);
            var result = badRequestController.GetBadRequestWithModelState(modelState);

            var middleware =
                new WebApiErrorHandlingMiddleware(innerHttpContext => throw new NameValidationException(),
                    new FakeLogger());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.BadRequest, context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual, expected));
        }
    }
}