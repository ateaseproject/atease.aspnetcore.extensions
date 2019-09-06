using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Middleware.Test
{
    public class BadRequestController : Controller
    {
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        public ActionResult GetBadRequestWithError(string error)
        {
            return BadRequest(error);
        }

        public ActionResult GetBadRequestWithModelState(ModelStateDictionary modelState)
        {
            return BadRequest(modelState);
        }
    }
}