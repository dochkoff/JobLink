using JobLink.Core.Models.Company;

namespace JobLink.Core.Contracts
{
    public interface ICompanyService
    {
        Task<string> CreateCompanyAsync(CompanyFormModel model);

        Task<CompanyDetailsServiceModel> CompanyDetailsByIdAsync(string companyId);

        Task<bool> CompanyExistsAsync(string companyId);

        Task<AllCompaniesModel> AllApprovedCompaniesAsync();

        Task<AllCompaniesModel> AllNonApprovedCompaniesAsync();

        Task ApproveCompanyAsync(string companyId);

        Task RejectCompanyAsync(string companyId);
    }
}
