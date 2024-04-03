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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
