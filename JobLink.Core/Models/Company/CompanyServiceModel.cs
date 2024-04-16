using JobLink.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace JobLink.Core.Models.Company
{
    public class CompanyServiceModel : ICompanyModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string LogoUrl { get; set; } = string.Empty;

        [Display(Name = "Is Approved")]
        public bool IsApproved { get; set; }
    }
}
