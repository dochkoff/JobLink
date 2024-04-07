using JobLink.Attributes;
using JobLink.Core.Contracts;
using JobLink.Core.Models.Employer;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static JobLink.Core.Constants.MessageConstants;

namespace JobLink.Controllers
{
    public class EmployerController : BaseController
    {
        private readonly IEmployerService employerService;
        private readonly ICompanyService companyService;

        public EmployerController(IEmployerService _employerService, ICompanyService _companyService)
        {
            employerService = _employerService;
            companyService = _companyService;
        }

        [HttpGet]
        [NotAnEmployer]
        public async Task<IActionResult> BecomeEmployer()
        {
            if (await employerService.UserHasApplicationsAsync(User.Id()))
            {
                ModelState.AddModelError("Error", HasApplications);
            }

            var model = new BecomeEmployerFormModel()
            {
                Companies = await companyService.AllCompaniesAsync()
            };
   
            return View(model);
        }

        [HttpPost]
        [NotAnEmployer]
        public async Task<IActionResult> BecomeEmployer(BecomeEmployerFormModel model)
        {
            if (await employerService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
            }

            if (await employerService.UserHasApplicationsAsync(User.Id()))
            {
                ModelState.AddModelError("Error", HasApplications);
            }

            if (await employerService.CompanyWithIdAndNameExistsAsync(model.CompanyName, model.CompanyId)==false)
            {
                ModelState.AddModelError("Error", WrongCompanyOrId);
            }

            if (ModelState.IsValid == false)
            {
                model.Companies = await companyService.AllCompaniesAsync();

                return View(model);
            }

            await employerService.CreateAsync(User.Id(), model.PhoneNumber, model.CompanyId);

            return RedirectToAction(nameof(JobController.Board), "Job");
        }


        [HttpGet]
        [MustBeEmployer]
        public async Task<IActionResult> MyJobPosts()
        {
            int employerId = await employerService.GetEmployerIdAsync(User.Id()) ?? 0;

            var model = await employerService.AllJobPostsByEmployerIdAsync(employerId);

            return View(model);
        }
    }
}