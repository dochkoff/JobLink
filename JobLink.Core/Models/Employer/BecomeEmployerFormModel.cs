using JobLink.Core.Models.Company;
using System.ComponentModel.DataAnnotations;
using static JobLink.Core.Constants.MessageConstants;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Core.Models.Employer
{
    public class BecomeEmployerFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(PhoneMaxLength,
            MinimumLength = PhoneMinLength,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CompanyIdMaxLength,
            MinimumLength = CompanyIdMinLength,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Company ID")]
        public string CompanyId { get; set; } = string.Empty;

        public IEnumerable<CompanyServiceModel> Companies { get; set; } = new List<CompanyServiceModel>();

    }
}
