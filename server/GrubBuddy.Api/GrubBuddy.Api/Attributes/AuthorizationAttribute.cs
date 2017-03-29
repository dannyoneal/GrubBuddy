using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Auth0.Core;
using GrubBuddy.DataAccess;
using GrubBuddy.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GrubBuddy.Api.Attributes
{
    public class AuthorizationAttribute : ActionFilterAttribute
    {
        private readonly IUserRepository _userRepo;
        public AuthorizationAttribute(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_userRepo.DoesUserExist(context.HttpContext.Request.Headers["x-grubbuddy-userid"])) return;

            context.Result = new BadRequestObjectResult(new ApiResponse
            {
                Errors = new[] {"User is not authorized or User does not exist!"}
            });
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
        }
    }
}
