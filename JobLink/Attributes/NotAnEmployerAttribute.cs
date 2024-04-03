using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using JobLink.Core.Contracts;

namespace JobLink.Attributes
{
    public class NotAnEmployerAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    base.OnActionExecuting(context);

        //    IEmployerService? agentService = context.HttpContext.RequestServices.GetService<IEmployerService>();

        //    if (agentService == null)
        //    {
        //        context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
        //    }

        //    if (agentService != null
        //        && agentService.ExistsByIdAsync(context.HttpContext.User.Id()).Result)
        //    {
        //        context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
        //    }
        //}
    }
}
