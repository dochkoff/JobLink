using JobLink.Core.Contracts;
using System.ComponentModel.DataAnnotations;
using static JobLink.Core.Constants.MessageConstants;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Core.Models.Company
{
    public class CompanyFormModel : ICompanyModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [MaxLength(CompanyNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(LocationMaxLength,
            MinimumLength = LocationMinLength,
            ErrorMessage = LengthMessage)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(PhoneMaxLength,
            MinimumLength = PhoneMinLength,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(UrlMaxLength,
            MinimumLength = UrlMinLength,
            ErrorMessage = LengthMessage)]
        public string Website { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(UrlMaxLength,
            MinimumLength = UrlMinLength,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Company Logo URL")]
        public string LogoUrl { get; set; } = string.Empty;
    }
}
