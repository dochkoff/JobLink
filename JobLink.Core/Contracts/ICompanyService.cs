using JobLink.Core.Models.Company;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Core.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyServiceModel>> AllCompaniesAsync();

        Task<int?> GetCompanyByIdAsync(int companyId); //not used yet

        Task<int> GetCompanyIdByName(string companyName);  //not used yet
    }
}
