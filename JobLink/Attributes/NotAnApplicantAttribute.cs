using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using JobLink.Core.Contracts;
using System.Security.Claims;

namespace JobLink.Attributes
{
    public class NotAnApplicantAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            IApplicantService? applicantService = context.HttpContext.RequestServices.GetService<IApplicantService>();

            if (applicantService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (applicantService != null
                && applicantService.ExistsByIdAsync(context.HttpContext.User.Id()).Result)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
