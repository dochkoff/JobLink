using JobLink.Core.Contracts;
using JobLink.Core.Models.Job;
using Microsoft.AspNetCore.Mvc;

namespace JobLink.Areas.Admin.Controllers
{
    public class JobController : AdminBaseController
    {
        private readonly IJobService jobService;

        public JobController(
            IJobService _jobService)
        {
            jobService = _jobService;
        }

        [HttpGet]
        public async Task<IActionResult> AllJobPosts([FromQuery] AllJobsQueryModel model)
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
    }
}
