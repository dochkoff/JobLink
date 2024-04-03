using JobLink.Core.Models.Employer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Controllers
{
    [Authorize]
    public class EmployerController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            var model = new BecomeEmployerFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeEmployerFormModel model)
        {
            return RedirectToAction(nameof(JobController.Board), "Job");
        }
    }
}
