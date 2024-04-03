using JobLink.Core.Models.Applicant;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Controllers
{
    public class ApplicantController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            var model = new BecomeApplicantFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeApplicantFormModel model)
        {
            return RedirectToAction(nameof(JobController.Board), "Job");
        }
    }
}
