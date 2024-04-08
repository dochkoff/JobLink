using Microsoft.AspNetCore.Mvc;

namespace JobLink.Controllers
{
    public class CompanyController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}