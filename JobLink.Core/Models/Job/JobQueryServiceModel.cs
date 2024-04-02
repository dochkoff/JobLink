namespace JobLink.Core.Models.Job
{
    public class JobQueryServiceModel
    {
        public int TotalJobsCount { get; set; }

        public IEnumerable<JobServiceModel> Jobs { get; set; } = new List<JobServiceModel>();
    }
}
