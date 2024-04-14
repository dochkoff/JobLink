using JobLink.Core.Contracts;

namespace JobLink.Core.Models.Company
{
    public class CompanyServiceModel : ICompanyModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
