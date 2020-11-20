using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling.DefaultMappers;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling
{
    public static class WebApiErrorHandlingConfigExtension
    {
        public static void CatchAllArgumentException(this WebApiErrorHandlingConfig config)
        {
            config.CatchArgumentException();
            config.CatchArgumentNullException();
            config.CatchArgumentOutOfRangeException();
        }

        public static void CatchArgumentException(this WebApiErrorHandlingConfig config)
        {
            config.MapBadRequest(new ArgumentExceptionMapper());
        }

        public static void CatchArgumentNullException(this WebApiErrorHandlingConfig config)
        {
            config.MapBadRequest(new ArgumentNullExceptionMapper());
        }

        public static void CatchArgumentOutOfRangeException(this WebApiErrorHandlingConfig config)
        {
            config.MapBadRequest(new ArgumentOutOfRangeExceptionMapper());
        }
    }
}