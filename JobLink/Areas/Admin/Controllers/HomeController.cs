using JobLink.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        private readonly ICompanyService companyService;

        public HomeController(ICompanyService _companyService)
        {
            companyService = _companyService;
        }

        [HttpGet]
        public async Task<IActionResult> AdminCenter()
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

            await companyService.ApproveCompanyAsync(companyId);

            return RedirectToAction(nameof(AwaitingApproval), "Home");
        }
    }
}
