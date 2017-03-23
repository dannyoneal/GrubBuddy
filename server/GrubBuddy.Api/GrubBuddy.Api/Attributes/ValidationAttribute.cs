using System.Linq;
using GrubBuddy.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GrubBuddy.Api.Attributes
{
    public class ValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid) return;

            //actionContext.Result = new BadRequestObjectResult(new ApiResponse
            //{
            //    Errors = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage),
            //    Result = actionContext.ActionArguments.Values
            //});
        }
    }
}
