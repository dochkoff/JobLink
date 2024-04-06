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
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
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
        public async Task<IActionResult> MyApplications()
        {
            var userId = User.Id();
            IEnumerable<JobServiceModel> model;

            if (await employerService.ExistsByIdAsync(userId))
            {
                int employerId = await employerService.GetEmployerIdAsync(userId) ?? 0;
                model = await jobService.AllJobsByEmployerIdAsync(employerId);
            }
            else
            {
                model = await jobService.AllJobsByUserIdAsync(userId);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, string information)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await jobService.JobDetailsByIdAsync(id);

            if (information != model.GetInformation())
            {
                return BadRequest();
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

            int newJobId = await jobService.CreateAsync(model, employerId ?? 0);

            return RedirectToAction(nameof(Details), new { id = newJobId, information = model.GetInformation() });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

            return View(model);
        }

        [HttpPost]
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
        public async Task<IActionResult> Delete(int id)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await jobService.HasEmployerWithIdAsync(id, User.Id()) == false)
            {
                return Unauthorized();
            }

            var house = await jobService.JobDetailsByIdAsync(id);

            var model = new JobDetailsViewModel()
            {
                Id = id,
                Title = house.Title,
                Location = house.Location
            };

            return View(model);
        }

        [HttpPost]
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

        [HttpPost]
        public async Task<IActionResult> Apply(int id)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            if (await employerService.ExistsByIdAsync(User.Id()))
            {
                return Unauthorized();
            }

            if (await jobService.IsAppliedAsync(id))
            {
                return BadRequest();
            }

            await jobService.ApplyAsync(id, User.Id());

            return RedirectToAction(nameof(Board));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            if (await jobService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            try
            {
                await jobService.CancelAsync(id, User.Id());
            }
            catch (UnauthorizedActionException uae)
            {
                //logger.LogError(uae, "HouseController/Cancel");

                return Unauthorized();
            }


            return RedirectToAction(nameof(Board));
        }
    }
}
