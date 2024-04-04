using JobLink.Attributes;
using JobLink.Core.Contracts;
using JobLink.Core.Models.Employer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static JobLink.Core.Constants.MessageConstants;

namespace JobLink.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Become()
        {
            var model = new BecomeEmployerFormModel()
            {
                Companies = await companyService.AllCompaniesAsync()
            };
   

            return View(model);
        }

        [HttpPost]
        [NotAnEmployer]
        public async Task<IActionResult> Become(BecomeEmployerFormModel model)
        {
            if (await employerService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
            }

            if (await employerService.UserHasApplicationsAsync(User.Id()))
            {
                ModelState.AddModelError("Error", HasApplications);
            }

            if (ModelState.IsValid == false)
            {
                model.Companies = await companyService.AllCompaniesAsync();

                return View(model);
            }

            //int? agentId = await agentService.GetAgentIdAsync(User.Id());
            //int newHouseId = await houseService.CreateAsync(model, agentId ?? 0);


            await employerService.CreateAsync(User.Id(), model.PhoneNumber, model.CompanyId);

            return RedirectToAction(nameof(JobController.Board), "Job");
        }
    }
}