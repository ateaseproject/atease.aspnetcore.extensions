﻿using System.Collections.Generic;
using AtEase.AspNetCore.ApiErrorHandling.ApiErrorHandling;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling
{
    public class WebApiErrorHandlingConfig
    {
        private IList<WebApiErrorHandlingMapper> _badRequestMappers;

        public IList<WebApiErrorHandlingMapper> Mappers =>
            _badRequestMappers ?? (_badRequestMappers = new List<WebApiErrorHandlingMapper>());


        public void Map(WebApiErrorHandlingMapper mapper)
        {
            Mappers.Add(mapper);
        }
    }

    public static class WebApiErrorHandlingConfigExtension
    {
        public static void CatchAllArgumentExceptions(this WebApiErrorHandlingConfig config)
        {
            config.CatchArgumentException();
            config.CatchArgumentNullException();
            config.CatchArgumentOutOfRangeException();
        }

        public static void CatchArgumentException(this WebApiErrorHandlingConfig config)
        {
            config.Map(new ArgumentExceptionMapper());
        }

        public static void CatchArgumentNullException(this WebApiErrorHandlingConfig config)
        {
            config.Map(new ArgumentNullExceptionMapper());
        }

        public static void CatchArgumentOutOfRangeException(this WebApiErrorHandlingConfig config)
        {
            config.Map(new ArgumentOutOfRangeExceptionMapper());
        }
    }
}