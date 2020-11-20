using System.Collections.Generic;
using AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling.DefaultMappers;

namespace AtEase.AspNetCore.Extensions.Middleware.ApiErrorHandling
{
    public class WebApiErrorHandlingConfig
    {
        private IList<WebApiErrorHandlingMapper> _badRequestMappers;

        public IList<WebApiErrorHandlingMapper> Mappers =>
            _badRequestMappers ?? (_badRequestMappers = new List<WebApiErrorHandlingMapper>());

        public bool CatchAllArgumentsException { get; set; }
        public bool CatchArgumentNullException { get; set; }

        public bool CatchArgumentOutOfRangeException { get; set; }
        public bool CatchArgumentException { get; set; }

        public void MapBadRequest(WebApiErrorHandlingMapper mapper)
        {
            Mappers.Add(mapper);
        }
    }
}