using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace JobLink.Core.Models.Applicant
{
    public class ApplicantServiceModel
    {
        public string Name { get; set; } = null!;

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Display(Name = "Resume URL")]
        public string ResumeUrl { get; set; } = null!;
    }
}
