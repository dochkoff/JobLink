using JobLink.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Areas.Admin.Controllers
{
    public class EmployerController : AdminBaseController
    {   
        private readonly IEmployerService employerService;

        public EmployerController(IEmployerService _employerService)
        {
            employerService = _employerService;
        }

        public async Task<IActionResult> AllEmployers()
        {
            var model = await employerService.AllEmployersAsync();

            return View(model);
        }
    }
}
