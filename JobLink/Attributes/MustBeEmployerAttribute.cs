﻿using JobLink.Controllers;
using JobLink.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

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
                && employerService.EmployerExistsByIdAsync(context.HttpContext.User.Id()).Result == false)
            {
                context.Result = context.Result = new RedirectToActionResult(nameof(EmployerController.BecomeEmployer), "Employer", null);
            }
        }
    }
}
