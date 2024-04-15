using System.ComponentModel.DataAnnotations;
using static JobLink.Core.Constants.MessageConstants;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Core.Models.Applicant
{
    public class BecomeApplicantFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(PhoneMaxLength,
            MinimumLength = PhoneMinLength,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(UrlMaxLength,
    MinimumLength = UrlMinLength,
    ErrorMessage = LengthMessage)]
        [Display(Name = "Resume URL")]
        public string ResumeUrl { get; set; } = null!;
    }
}
