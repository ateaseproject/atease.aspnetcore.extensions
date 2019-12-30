using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Middleware.Test
{
    public class ConflictController : Controller
    {
        public ActionResult GetConflict()
        {
            return Conflict();
        }

        public ActionResult GetConflictWithError(string error)
        {
            return Conflict(error);
        }

        public ActionResult GetConflictWithModelState(ModelStateDictionary modelState)
        {
            return Conflict(modelState);
        }
    }
}