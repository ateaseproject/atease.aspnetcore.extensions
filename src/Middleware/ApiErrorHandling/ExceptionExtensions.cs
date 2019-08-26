using System;
using System.Linq;
using AtEase.Extensions;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public static class ExceptionExtensions
    {
        public static bool TryGetApiExceptionAttribute(this Exception exception,
            out ApiExceptionAttribute apiExceptionAttribute)
        {
            var attr = exception.GetType().GetCustomAttributes(
                typeof(ApiExceptionAttribute), true
            ).SingleOrDefault();
            if (attr.IsNotNull())
            {
                apiExceptionAttribute = attr as ApiExceptionAttribute;
                return true;
            }

            apiExceptionAttribute = null;
            return false;
        }


        public static bool TryGetApiValidationExceptionAttribute(this Exception exception,
            out ApiValidationExceptionAttribute apiValidationExceptionAttribute)
        {
            var attr = exception.GetType().GetCustomAttributes(
                typeof(ApiValidationExceptionAttribute), true
            ).SingleOrDefault();
            if (attr.IsNotNull())
            {
                apiValidationExceptionAttribute = attr as ApiValidationExceptionAttribute;
                return true;
            }

            apiValidationExceptionAttribute = null;
            return false;
        }
    }
}