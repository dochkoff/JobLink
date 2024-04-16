namespace JobLink.Core.Models.Company
{
    public class AllCompaniesModel
    {
        public IEnumerable<CompanyServiceModel> Companies { get; set; } = new List<CompanyServiceModel>();
    }
}
