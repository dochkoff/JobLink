using JobLink.Core.Contracts;
using JobLink.Core.Models.Statistics;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository repository;

        public StatisticService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<StatisticServiceModel> TotalAsync()
        {
            int totalCompanies = await repository.AllReadOnly<Company>()
                .Where(c => c.IsApproved == true)
                .CountAsync();

            int totalJobs = await repository.AllReadOnly<Job>()
                .Where(j => j.Employer.Company.IsApproved == true)
                .CountAsync();

            return new StatisticServiceModel()
            {
                TotalCompanies = totalCompanies,
                TotalJobs = totalJobs
            };
        }
    }
}
