using JobLink.Core.Models.Employer;

namespace JobLink.Core.Models.Job
{
    public class JobDetailsServiceModel : JobServiceModel
    {
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;

        public EmployerServiceModel Employer { get; set; } = null!;
    }
}
