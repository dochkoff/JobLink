using JobLink.Core.Contracts;
using JobLink.Core.Extensions;
using JobLink.Core.Models.Company;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Controllers
{
    public class CompanyController : BaseController
    {

        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService _companyService)
        {
            companyService = _companyService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
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