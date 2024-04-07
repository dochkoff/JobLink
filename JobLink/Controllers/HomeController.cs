using JobLink.Core.Contracts;
using JobLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobLink.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;
        private readonly IJobService jobService;

        public HomeController(
            ILogger<HomeController> _logger,
            IJobService _jobService)
        {
            logger = _logger;
            jobService = _jobService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await jobService.LatestJobsAsync();

            return View(model);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {

            if (statusCode == 400)
            {
                return View("Error400");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
