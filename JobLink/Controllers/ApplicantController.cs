using JobLink.Attributes;
using JobLink.Core.Contracts;
using JobLink.Core.Models.Applicant;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static JobLink.Core.Constants.MessageConstants;

namespace JobLink.Controllers
{
    public class ApplicantController : BaseController
    {
        private readonly IApplicantService applicantService;
        private readonly IEmployerService employerService;

        public ApplicantController(IApplicantService _applicantService, IEmployerService _employerService)
        {
            applicantService = _applicantService;
            employerService = _employerService;
        }

        [HttpGet]
        [NotAnApplicant]
        [NotAnEmployer]
        public async Task<IActionResult> BecomeApplicant()
        {
            var model = new BecomeApplicantFormModel();

            return View(model);
        }

        [HttpPost]
        [NotAnApplicant]
        [NotAnEmployer]
        public async Task<IActionResult> BecomeApplicant(BecomeApplicantFormModel model)
        {
            if (await applicantService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
            }

            if (await employerService.EmployerExistsByIdAsync(User.Id()))
            {
                ModelState.AddModelError("Error", UserIsEmployer);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await applicantService.CreateAsync(User.Id(),model.Name, model.PhoneNumber, model.ResumeUrl);

            return RedirectToAction(nameof(JobController.MyJobApplications), "Job");
        }
    }
}
