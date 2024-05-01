using JobLink.Core.Contracts;
using JobLink.Core.Extensions;
using JobLink.Core.Models.Company;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobLink.Controllers
{
    public class CompanyController : BaseController
    {

        private readonly ICompanyService companyService;
        private readonly IEmployerService employerService;

        public CompanyController(
            ICompanyService _companyService, 
            IEmployerService _employerService)
        {
            companyService = _companyService;
            employerService = _employerService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await employerService.UserHasApplicationsAsync(User.Id()))
            {
                return Unauthorized();
            }

            var model = new CompanyFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyFormModel model)
        {
            string newCompanyId = await companyService.CreateCompanyAsync(model);

            return RedirectToAction(nameof(Details), new { companyId = newCompanyId, information = model.GetCompanyInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> StatusCheck()
        {
            if (await employerService.UserHasApplicationsAsync(User.Id()))
            {
                return Unauthorized();
            }

            var model = new CompanyStatusCheck();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StatusCheck(CompanyStatusCheck model)
        {
            string companyId = model.CompanyId;
            var company = await companyService.CompanyDetailsByIdAsync(companyId);

            return RedirectToAction(nameof(Details), new {  companyId, information = company.GetCompanyInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string companyId, string information)
        {
            if (await companyService.CompanyExistsAsync(companyId) == false)
            {
                return BadRequest();
            }

            var model = await companyService.CompanyDetailsByIdAsync(companyId);

            if (information != model.GetCompanyInformation())
            {
                return NotFound();
            }

            return View(model);
        }
    }
}