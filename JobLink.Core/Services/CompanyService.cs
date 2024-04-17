using JobLink.Core.Contracts;
using JobLink.Core.Exceptions;
using JobLink.Core.Models.Company;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public async Task<AllCompaniesModel> AllApprovedCompaniesAsync()
        {
            var companiesToShow = await repository.AllReadOnly<Company>()
                .Where(c => c.IsApproved == true)
                .Select(c => new CompanyServiceModel()
                {
                    Id = c.Id.ToString().ToLower(),
                    Name = c.Name,
                    Address = c.Address,
                    LogoUrl = c.LogoUrl,
                    IsApproved = c.IsApproved
                })
                .ToListAsync();

            return new AllCompaniesModel
            {
                Companies = companiesToShow
            };
        }

        public async Task<AllCompaniesModel> AllNonApprovedCompaniesAsync()
        {
            var companiesToShow = await repository.AllReadOnly<Company>()
                .Where(c => c.IsApproved == false)
                .Select(c => new CompanyServiceModel()
                {
                    Id = c.Id.ToString().ToLower(),
                    Name = c.Name,
                    Address = c.Address,
                    LogoUrl = c.LogoUrl,
                    IsApproved = c.IsApproved
                })
                .ToListAsync();

            return new AllCompaniesModel()
            {
                Companies = companiesToShow
            };
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
                    IsApproved = c.IsApproved
                })
                .FirstAsync();
                    
        }

        public async Task ApproveCompanyAsync(string companyId)
        {
            var company = await repository.All<Company>()
                .Where(c => c.Id.ToString().ToLower() == companyId.ToLower())
                .FirstAsync();

            if (company != null)
            {
                company.IsApproved = true;

                await repository.SaveChangesAsync();
            }
        }

        public async Task RejectCompanyAsync(string companyId)
        {
            var company = await repository.All<Company>()
                .Where(c => c.Id.ToString().ToLower() == companyId.ToLower())
                .FirstAsync();

            if (company != null)
            {
                company.IsApproved = false;

                await repository.SaveChangesAsync();
            }
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
