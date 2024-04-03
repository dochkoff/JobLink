using JobLink.Core.Models.Statistics;

namespace JobLink.Core.Contracts
{
    public interface IStatisticService
    {
        Task<StatisticServiceModel> TotalAsync();
    }
}
