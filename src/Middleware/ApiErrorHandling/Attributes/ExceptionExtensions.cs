using System;
using System.Linq;
using AtEase.Extensions;

namespace AtEase.AspNetCore.Extensions.Middleware
{
    public static class ExceptionExtensions
    {
        public static bool TryGetWebApiConflictAttribute(this Exception exception,
            out WebApiConflictAttribute webApiConflictAttribute)
        {
            var attr = exception.GetType().GetCustomAttributes(
                typeof(WebApiConflictAttribute), true
            ).SingleOrDefault();
            if (attr.IsNotNull())
            {
                webApiConflictAttribute = attr as WebApiConflictAttribute;
                return true;
            }

            webApiConflictAttribute = null;
            return false;
        }


        public static bool TryGetWebApiBadRequestAttribute(this Exception exception,
            out WebApiBadRequestAttribute webApiBadRequestAttribute)
        {
            var attr = exception.GetType().GetCustomAttributes(
                typeof(WebApiBadRequestAttribute), true
            ).SingleOrDefault();
            if (attr.IsNotNull())
            {
                webApiBadRequestAttribute = attr as WebApiBadRequestAttribute;
                return true;
            }

            webApiBadRequestAttribute = null;
            return false;
        }
    }
}