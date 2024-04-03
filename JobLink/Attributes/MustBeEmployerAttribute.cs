﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JobLink.Core.Contracts;
using JobLink.Controllers;

namespace JobLink.Attributes
{
    public class MustBeEmployerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            IEmployerService? employerService = context.HttpContext.RequestServices.GetService<IEmployerService>();

            if (employerService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (employerService != null
                && employerService.ExistsByIdAsync(context.HttpContext.User.Id()).Result == false)
            {
                context.Result = new RedirectToActionResult(nameof(EmployerController.Become), "Employer", null);
            }
        }
    }
}
