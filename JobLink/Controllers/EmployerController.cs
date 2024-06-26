﻿using JobLink.Attributes;
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
        private readonly IJobService jobService;
        private readonly ICompanyService companyService;

        public EmployerController(
            IEmployerService _employerService,
            ICompanyService _companyService,
            IJobService _jobService)
        {
            employerService = _employerService;
            companyService = _companyService;
            jobService = _jobService;
        }

        [HttpGet]
        [NotAnEmployer]
        public async Task<IActionResult> BecomeEmployer()
        {
            if (await employerService.UserHasApplicationsAsync(User.Id()))
            {
                ModelState.AddModelError("Error", HasApplications);
            }

            var companiesToShow = await companyService.AllApprovedCompaniesAsync();

            var model = new BecomeEmployerFormModel()
            {
                Companies = companiesToShow.Companies
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

            var companiestoShow = await companyService.AllApprovedCompaniesAsync();

            if (ModelState.IsValid == false)
            {
                model.Companies = companiestoShow.Companies;

                return View(model);
            }

            await employerService.CreateEmployerAsync(User.Id(), model.PhoneNumber, model.CompanyId);

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

        [HttpGet]
        [MustBeEmployer]
        public async Task<IActionResult> MyJobPostApplications(int jobId)
        {
            if (await jobService.ExistsAsync(jobId) == false)
            {
                return BadRequest();
            }
            var model = await employerService.AllApplicationsByJobIdAsync(jobId);

            if (model.Count() == 0)
            {
                return BadRequest();
            }

            return View(model);
        }
    }
}