using JobLink.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Areas.Admin.Controllers
{
    public class ApplicantController : AdminBaseController
    {
        private readonly IApplicantService applicantService;

        public ApplicantController(IApplicantService _applicantService)
        {
            applicantService = _applicantService;
        }

        public async Task<IActionResult> AllApplicants()
        {
            var model = await applicantService.AllApplicantsAsync();

            return View(model);
        }
    }
}