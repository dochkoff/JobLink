using JobLink.Core.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace JobLink.Core.Models.Job
{
    public class AllJobsQueryModel
    {
        public int JobsPerPage { get; } = 3;

        [Display(Name = "Job Category")]
        public string JobCategory { get; init; } = null!;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; } = null!;

        public JobSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalJobsCount { get; set; }

        public IEnumerable<string> JobCategories { get; set; } = null!;

        public IEnumerable<JobServiceModel> Jobs { get; set; } = new List<JobServiceModel>();
    }
}
