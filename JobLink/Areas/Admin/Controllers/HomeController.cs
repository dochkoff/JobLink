using JobLink.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobLink.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        [HttpGet]
        public IActionResult AdminCenter()
        {
            if (User.IsAdmin() == false)
            {
                return Unauthorized();
            }
            return View();
        }

        [HttpGet]
        public IActionResult ApproveCompany()
        {

            return View();
        }
    }
}
