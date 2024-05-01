using JobLink.Core.Contracts;
using JobLink.Core.Extensions;
using JobLink.Core.Models.Company;
using JobLink.Core.Models.Job;
using JobLink.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Areas.Admin.Controllers
{
    public class CompanyController : AdminBaseController
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService _companyService)
        {
            companyService = _companyService;
        }

        [HttpGet]
        public async Task<IActionResult> AllCompanies()
        {
            var model = await companyService.AllApprovedCompaniesAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AwaitingApproval()
        {
            var model = await companyService.AllNonApprovedCompaniesAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveCompany(string companyId)
        {
            if (await companyService.CompanyExistsAsync(companyId) == false)
            {
                return BadRequest();
            }

            var company = await companyService.CompanyDetailsByIdAsync(companyId);

            var model = new CompanyDetailsServiceModel()
            {
                Id = companyId,
                Name = company.Name,
                Address = company.Address,
                LogoUrl = company.LogoUrl,
                IsApproved = company.IsApproved
            };

            await companyService.ApproveCompanyAsync(companyId);

            return RedirectToAction(nameof(Details),
                new { area = "Admin", companyId, information = model.GetCompanyInformation() });
        }

        [HttpPost]
        public async Task<IActionResult> RejectCompany(string companyId)
        {
            if (await companyService.CompanyExistsAsync(companyId) == false)
            {
                return BadRequest();
            }

            var company = await companyService.CompanyDetailsByIdAsync(companyId);

            var model = new CompanyDetailsServiceModel()
            {
                Id = companyId,
                Name = company.Name,
                Address = company.Address,
                LogoUrl = company.LogoUrl,
                IsApproved = company.IsApproved
            };

            await companyService.RejectCompanyAsync(companyId);

            return RedirectToAction(nameof(Details),
                new { area = "Admin", companyId, information = model.GetCompanyInformation() });
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
