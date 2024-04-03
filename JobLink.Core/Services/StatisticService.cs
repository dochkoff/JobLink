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
            int totalJobs = await repository.AllReadOnly<Job>()
                .CountAsync();

            int totalEmployers = await repository.AllReadOnly<Employer>()
                .CountAsync();

            return new StatisticServiceModel()
            {
                TotalJobs = totalJobs,
                TotalEmployers = totalEmployers
            };
        }
    }
}
