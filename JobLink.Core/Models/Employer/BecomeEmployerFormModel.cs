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
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Company")]
        public int CompanyId { get; set; }

        public IEnumerable<CompanyServiceModel> Companies { get; set; } = new List<CompanyServiceModel>();

    }
}
