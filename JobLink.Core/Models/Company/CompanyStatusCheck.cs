using System.ComponentModel.DataAnnotations;
using static JobLink.Core.Constants.MessageConstants;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Core.Models.Company
{
    public class CompanyStatusCheck
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(CompanyIdMaxLength,
            MinimumLength = CompanyIdMinLength,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Company ID")]
        public string CompanyId { get; set; } = string.Empty;
    }
}
