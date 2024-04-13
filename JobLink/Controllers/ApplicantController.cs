using JobLink.Attributes;
using JobLink.Core.Contracts;
using JobLink.Core.Exceptions;
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
        private readonly IJobService jobService;

        public ApplicantController(
            IApplicantService _applicantService,
            IEmployerService _employerService,
            IJobService _jobService)
        {
            applicantService = _applicantService;
            employerService = _employerService;
            jobService = _jobService;
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

            await applicantService.CreateApplicantAsync(User.Id(),model.Name, model.PhoneNumber, model.ResumeUrl);

            return RedirectToAction(nameof(JobController.Board), "Job");
        }

        [HttpPost]
        [MustBeApplicant]
        public async Task<IActionResult> Apply(int id)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await employerService.EmployerExistsByIdAsync(User.Id()))
            {
                return Unauthorized();
            }

            await jobService.ApplyAsync(id, User.Id());

            return RedirectToAction(nameof(JobController.Board), "Job");
        }

        [HttpPost]
        [MustBeApplicant]
        public async Task<IActionResult> Cancel(int id)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }
            if (await employerService.EmployerExistsByIdAsync(User.Id()))
            {
                return Unauthorized();
            }

            //try
            //{
            //    await jobService.CancelAsync(id, User.Id());
            //}
            //catch (UnauthorizedActionException uae)
            //{
            //    logger.LogError(uae, "HouseController/Cancel");

            //    return Unauthorized();
            //}

            return RedirectToAction(nameof(MyJobApplications));
        }

        [HttpGet]
        [MustBeApplicant]
        public async Task<IActionResult> MyJobApplications()
        {
            var model = await applicantService.AllJobApplicationsByUserIdAsync(User.Id());

            return View(model);
        }
    }
}
