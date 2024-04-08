using JobLink.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

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
                && applicantService.ApplicantExistsByIdAsync(context.HttpContext.User.Id()).Result == false)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }
    }
}
