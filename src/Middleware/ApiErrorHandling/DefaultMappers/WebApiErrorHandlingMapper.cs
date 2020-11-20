using System;
using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling.DefaultMappers
{
    public abstract class WebApiErrorHandlingMapper
    {
        public abstract HttpStatusCode GetStatusCode();


        public abstract string GetReasonPhrase();

        protected static ModelStateDictionary CreateModelState(string fieldName, string message)
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError(fieldName,
                                     message);

            return modelState;
        }

        public abstract bool CanHandle(Exception exception);
        public abstract object CreateContent(Exception exception);
    }
}