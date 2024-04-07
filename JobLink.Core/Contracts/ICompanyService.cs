using JobLink.Core.Models.Company;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Core.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyServiceModel>> AllCompaniesAsync();

        Task<string> GetCompanyByIdAsync(string companyId); //not used yet

        Task<string> GetCompanyIdByName(string companyName);  //not used yet
    }
}
