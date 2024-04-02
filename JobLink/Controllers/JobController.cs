using JobLink.Core.Models.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Controllers
{
    [Authorize]
    public class JobController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Board()
        {
            var model = new AllJobsQueryModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyApplications()
        {
            var model = new AllJobsQueryModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> MyJobPosts()
        {
            var model = new AllJobsQueryModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new JobDetailsViewModel();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(JobFormModel model)
        {
            return RedirectToAction(nameof(Details), new {id = 1});
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new JobFormModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, JobFormModel model)
        {
            return RedirectToAction(nameof(Details), new {id = 1});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = new JobDetailsViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(JobDetailsViewModel model)
        {
            return RedirectToAction(nameof(Board));
        }

        [HttpPost]
        public async Task<IActionResult> Apply(int id)
        {
            return RedirectToAction(nameof(Board));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            return RedirectToAction(nameof(Board));
        }
    }
}
