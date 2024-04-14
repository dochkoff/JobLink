using JobLink.Core.Models.Company;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Core.Contracts
{
    public interface ICompanyService
    {
        Task<string> CreateCompanyAsync(CompanyFormModel model);

        Task<CompanyDetailsServiceModel> CompanyDetailsByIdAsync(string companyId);

        Task<bool> CompanyExistsAsync(string companyId);

        Task<IEnumerable<CompanyServiceModel>> AllCompaniesAsync();

        Task<string> GetCompanyByIdAsync(string companyId); //not used yet

        Task<string> GetCompanyIdByNameAsync(string companyName);  //not used yet


    }
}
