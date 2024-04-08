using JobLink.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

    }
}
