using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JobLink.Core.Contracts;
using JobLink.Controllers;

namespace JobLink.Attributes
{
    public class MustBeApplicantAttribute : ActionFilterAttribute
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
                && applicantService.ExistsByIdAsync(context.HttpContext.User.Id()).Result == false)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
        }
    }
}
