using JobLink.Core.Contracts;
using JobLink.Core.Models.Company;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

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
                    Id = c.Id.ToString().ToLower(),
                    Name = c.Name,
                    IsActive = c.IsActive
                })
                .ToListAsync();
        }

        public async Task<CompanyDetailsServiceModel> CompanyDetailsByIdAsync(string companyId)
        {
            return await repository.AllReadOnly<Company>()
                .Where(c => c.Id.ToString().ToLower() == companyId.ToLower())
                .Select(c => new CompanyDetailsServiceModel()
                {
                    Id = c.Id.ToString().ToLower(),
                    Name = c.Name,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    Website = c.Website,
                    LogoUrl = c.LogoUrl,
                    IsActive = c.IsActive
                })
                .FirstAsync();
                    
        }
       
        public async Task<string> GetCompanyByIdAsync(string companyId)
        {
            return (await repository.AllReadOnly<Company>()
                    .FirstOrDefaultAsync(c => c.Id.ToString().ToLower() == companyId.ToLower()))?.Name ?? string.Empty;
        }

        public async Task<string> GetCompanyIdByNameAsync(string companyName)
        {
            var company = await repository.AllReadOnly<Company>()
                .Where(c => c.Name == companyName)
                .FirstOrDefaultAsync();

            return company?.Id.ToString().ToLower() ?? string.Empty;
        }
        public async Task<bool> CompanyExistsAsync(string companyId)
        {
            return await repository.AllReadOnly<Company>()
                .AnyAsync(c => c.Id.ToString().ToLower() == companyId.ToLower());
        }

        public async Task<string> CreateCompanyAsync(CompanyFormModel model)
        {
            Company company = new Company()
            {
                Name = model.Name,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Website = model.Website,
                LogoUrl = model.LogoUrl
            };

            await repository.AddAsync(company);
            await repository.SaveChangesAsync();

            return company.Id.ToString();
        }
    }
}
