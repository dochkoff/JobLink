using JobLink.Attributes;
using JobLink.Core.Contracts;
using JobLink.Core.Exceptions;
using JobLink.Core.Extensions;
using JobLink.Core.Models.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static JobLink.Core.Constants.MessageConstants;

namespace JobLink.Controllers
{
    public class JobController : BaseController
    {
        private readonly IJobService jobService;
        private readonly IEmployerService employerService;

        public JobController(
            IJobService _jobService,
            IEmployerService _employerService)
        {
            jobService = _jobService;
            employerService = _employerService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Board([FromQuery] AllJobsQueryModel model)
        {
            var jobs = await jobService.AllAsync(
                model.JobCategory,
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                model.JobsPerPage);

            model.TotalJobsCount = jobs.TotalJobsCount;
            model.Jobs = jobs.Jobs;
            model.JobCategories = await jobService.AllCategoriesNamesAsync();

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id, string information)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await jobService.JobDetailsByIdAsync(id);

            if (information != model.GetInformation())
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        [MustBeEmployer]
        public async Task<IActionResult> Add()
        {
            var model = new JobFormModel()
            {
                Categories = await jobService.AllCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        [MustBeEmployer]
        public async Task<IActionResult> Add(JobFormModel model)
        {
            if (await jobService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), CategoryDoNotExist);
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = await jobService.AllCategoriesAsync();

                return View(model);
            }

            int? employerId = await employerService.GetEmployerIdAsync(User.Id());

            int newJobId = await jobService.CreateJobAsync(model, employerId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newJobId, information = model.GetInformation() });
        }

        [HttpGet]
        [MustBeEmployer]
        public async Task<IActionResult> Edit(int id, string information)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await jobService.HasEmployerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var model = await jobService.GetJobFormModelByIdAsync(id);

            if (information != model?.GetInformation())
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [MustBeEmployer]
        public async Task<IActionResult> Edit(int id, JobFormModel model)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await jobService.HasEmployerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            if (await jobService.CategoryExistsAsync(model.CategoryId) == false)
            {
                ModelState.AddModelError(nameof(model.CategoryId), CategoryDoNotExist);
            }

            if (ModelState.IsValid == false)
            {
                model.Categories = await jobService.AllCategoriesAsync();

                return View(model);
            }

            await jobService.EditAsync(id, model);

            return RedirectToAction(nameof(Details), new { id, information = model.GetInformation() });
        }

        [HttpGet]
        [MustBeEmployer]
        public async Task<IActionResult> Delete(int id, string information)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await jobService.HasEmployerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var job = await jobService.JobDetailsByIdAsync(id);

            var model = new JobDetailsViewModel()
            {
                Id = id,
                Title = job.Title,
                Location = job.Location,
                CompanyLogoURL = job.CompanyLogoURL
            };

            if (information != model.GetInformation())
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [MustBeEmployer]
        public async Task<IActionResult> Delete(JobDetailsViewModel model)
        {
            if (await jobService.ExistsAsync(model.Id) == false)
            {
                return BadRequest();
            }

            if (await jobService.HasEmployerWithIdAsync(model.Id, User.Id()) == false)
            {
                return Unauthorized();
            }

            await jobService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(Board));
        }
    }
}
