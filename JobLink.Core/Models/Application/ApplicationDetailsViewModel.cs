using JobLink.Core.Contracts;

namespace JobLink.Core.Models.Application
{
    public class ApplicationDetailsViewModel : IApplicationModel
    {
        public int Id { get; set;}

        public  string JobTitle { get; set;} = string.Empty;

        public string DateAndTime { get; set;} = string.Empty;

        public string ApplicantName { get; set; } = string.Empty;

        public string ApplicantPhoneNumber { get; set; } = string.Empty;

        public string ApplicantResumeUrl { get; set; } = string.Empty;
    }
}
