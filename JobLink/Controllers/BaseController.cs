using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {

            if (statusCode == 400)
            {
                return View("Error400");
            }
            else if (statusCode == 401)
            {
                return View("Error401");
            }
            else if (statusCode == 403)
            {
                return View("Error403");
            }
            else if (statusCode == 404)
            {
                return View("Error404");
            }
            else if (statusCode == 500)
            {
                return View("Error500");
            }
            else
            {
                return View();
            }
        }
    }
}
