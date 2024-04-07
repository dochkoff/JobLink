using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using JobLink.Core.Contracts;
using System.Security.Claims;

namespace JobLink.Attributes
{
    public class NotAnEmployerAttribute : ActionFilterAttribute
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
                && employerService.EmployerExistsByIdAsync(context.HttpContext.User.Id()).Result)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
