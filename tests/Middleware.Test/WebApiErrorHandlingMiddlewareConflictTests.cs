using System.IO;
using System.Net;
using System.Threading.Tasks;
using AtEase.Extensions;
using AtEase.Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Middleware.Test
{
    public class WebApiErrorHandlingMiddlewareConflictTests
    {
        [Fact]
        public async Task when_WebApiConflictException_raised_it_should_return_Conflict_http_status_code()
        {
            var conflictController = new ConflictController();

            var result = conflictController.GetConflict();

            var middleware =
                TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext => throw new EntityException());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();


            await middleware.Invoke(context);

            context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader     = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual   = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.Conflict,
                         context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }


        [Fact]
        public async Task
            when_WebApiConflictException_with_message_raised_it_should_return_Conflict_http_status_code()
        {
            const string error = "EntityNotFound";

            var conflictController = new ConflictController();

            var result = conflictController.GetConflictWithError(error);

            var middleware =
                TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext =>
                                                                  throw new EntityWithErrorMessageException());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();


            await middleware.Invoke(context);

            context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader     = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual   = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.Conflict,
                         context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }


        [Fact]
        public async Task
            when_WebApiConflictException_with_message_raised_it_should_return_Conflict_http_status_code2()
        {
            var exception = new EntityWithErrorMessageException();


            var conflictController = new ConflictController();


            var result = conflictController.GetConflictWithError(exception.Message);

            var middleware = TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext => throw exception);

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();


            await middleware.Invoke(context);

            context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader     = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual   = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.Conflict,
                         context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }

        [Fact]
        public async Task
            when_WebApiConflictException_with_ModelState_raised_it_should_return_Conflict_http_status_code()
        {
            const int    errorCode = -1;
            const string error     = "EntityNotFound";


            var conflictController = new ConflictController();

            var modelState = new ModelStateDictionary();
            modelState.AddModelError(errorCode.ToString(),
                                     error);
            var result = conflictController.GetConflictWithModelState(modelState);

            var middleware =
                TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext =>
                                                                  throw new EntityNotFoundExceptionWithMessage());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader     = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual   = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.Conflict,
                         context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }

        [Fact]
        public async Task
            when_WebApiConflictException_with_ModelState_raised_it_should_return_Conflict_http_status_code2()
        {
            const int    errorCode = -1;
            const string error     = "EntityNotFound";

            var conflictController = new ConflictController();

            var modelState = new ModelStateDictionary();
            modelState.AddModelError(errorCode.ToString(),
                                     error);
            var result = conflictController.GetConflictWithModelState(modelState);

            var middleware =
                TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext => throw new EntityNotFoundException());

            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader     = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual   = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.Conflict,
                         context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }
    }
}