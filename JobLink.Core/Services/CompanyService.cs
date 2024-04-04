using JobLink.Core.Contracts;
using JobLink.Core.Models.Company;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository repository;

        public CompanyService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<CompanyServiceModel>> AllCompaniesAsync()
        {
            return await repository.AllReadOnly<Company>()
                .Select(c => new CompanyServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<int?> GetCompanyByIdAsync(int companyId)
        {
            return (await repository.AllReadOnly<Company>()
                .FirstOrDefaultAsync(c => c.Id == companyId))?.Id;
        }

        public async Task<int> GetCompanyIdByName(string companyName)
        {
            var company = await repository.AllReadOnly<Company>()
                .Where(c => c.Name == companyName)
                .FirstOrDefaultAsync();

            return company?.Id ?? 0;
        }

    }
}
