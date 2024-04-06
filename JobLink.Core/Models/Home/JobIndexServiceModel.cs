using JobLink.Core.Contracts;

namespace JobLink.Core.Models.Home
{
    public class JobIndexServiceModel : IJobModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Employer { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
