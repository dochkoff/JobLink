using System.ComponentModel.DataAnnotations;

namespace JobLink.Core.Models.Company
{
    public class CompanyDetailsServiceModel : CompanyServiceModel
    {
        public string Website { get; set; } = string.Empty;

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Company Logo URL")]
        public string LogoUrl { get; set; } = string.Empty;
    }
}