using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling;
using AtEase.Extensions;
using AtEase.Newtonsoft.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Middleware.Test
{
    public class MappingTests
    {
  [Fact]
        public async Task when_ArgumentException_raised_it_should_BadRequest_http_status_code()
        {
            const string fieldName = "CatchArgumentException";
            const string error = "NameValidationException";


            var badRequestController = new BadRequestController();

            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName,
                                     error);
            var result = badRequestController.GetBadRequestWithModelState(modelState);

            var config = new WebApiErrorHandlingConfig();
            config.CatchArgumentException();
            var middleware =
                TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext =>
                                                                  throw new ArgumentException(error, fieldName),
                                                              config);
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.BadRequest,
                         context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }

//        [Fact]
//        public async Task
//            when_ArgumentOutOfRangeException_raised_but_config_is_set_to_argument_it_should_raise_exception_again()
//        {
//            const string fieldName = "ArgumentOutOfRange";
//            const string error = "NameValidationException";
//
//
//            var badRequestController = new BadRequestController();
//
//            var modelState = new ModelStateDictionary();
//            modelState.AddModelError(fieldName,
//                                     error);
//            var result = badRequestController.GetBadRequestWithModelState(modelState);
//
//            var config = new WebApiErrorHandlingConfig();
//            config.CatchArgumentException();
//            var middleware =
//                TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext =>
//                                                                  throw new CatchArgumentOutOfRangeException(
//                                                                  fieldName,
//                                                                  error),
//                                                              config);
//            var context = new DefaultHttpContext();
//            context.Response.Body = new MemoryStream();
//            Func<Task> act = async () => { await middleware.Invoke(context); };
//            await act.Should().ThrowAsync<CatchArgumentOutOfRangeException>();
//        }


        [Fact]
        public async Task when_ArgumentOutOfRangeException_raised_it_should_BadRequest_http_status_code()
        {
            const string fieldName = "ArgumentOutOfRange";
            const string error = "NameValidationException";


            var badRequestController = new BadRequestController();

            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName,
                                     error);
            var result = badRequestController.GetBadRequestWithModelState(modelState);

            var config = new WebApiErrorHandlingConfig();
            config.CatchArgumentOutOfRangeException();
            var middleware =
                TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext =>
                                                                  throw new ArgumentOutOfRangeException(
                                                                  fieldName,
                                                                  error),
                                                              config);
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();

            await middleware.Invoke(context);

            context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
            var streamText = reader.ReadToEnd();


            var expected = JToken.Parse(result.ToJson());
            var actual = JToken.Parse(streamText);


            Assert.Equal(HttpStatusCode.BadRequest,
                         context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }


        [Fact]
        public async Task when_ArgumentNullException_raised_it_should_BadRequest_http_status_code()
        {
            const string fieldName = "ArgumentNull";
            const string error = "NameValidationException";


            var badRequestController = new BadRequestController();

            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName,
                                     error);
            var result = badRequestController.GetBadRequestWithModelState(modelState);

            var config = new WebApiErrorHandlingConfig();
            

            config.CatchArgumentNullException();


            var middleware =
            TestHelper.BuildWebApiErrorHandlingMiddleware(innerHttpContext =>
                                                              throw new ArgumentNullException(fieldName,
                                                                  error),
                                                          config);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.Invoke(context);

        context.Response.Body.Seek(0,
                                       SeekOrigin.Begin);
            var reader = new StreamReader(context.Response.Body);
        var streamText = reader.ReadToEnd();


        var expected = JToken.Parse(result.ToJson());
        var actual = JToken.Parse(streamText);


        Assert.Equal(HttpStatusCode.BadRequest,
                     context.Response.StatusCode.AsEnum<HttpStatusCode>());

            Assert.True(JToken.DeepEquals(actual,
                                          expected));
        }

}





}