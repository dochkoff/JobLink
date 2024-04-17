using JobLink.Core.Contracts;
using JobLink.Core.Services;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Tests.UnitTests
{
    [TestFixture]
    public class StatisticServiceTests : UnitTestsBase
    {
        private IStatisticService _statisticService;
        private IRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Repository(_data);
            _statisticService = new StatisticService(_repository);
        }

        [Test]
        public async Task TotalAsync_ReturnsCorrectStatistics()
        {
            // Arrange
            // Seed some companies and jobs
            int totalComapnies = 2;
            int totalJobs = 2;

            // Act
            var result = await _statisticService.TotalAsync();

            // Assert
            // Assert that the total number of companies is correct
            Assert.AreEqual(2, result.TotalCompanies);

            // Assert that the total number of jobs is correct
            Assert.AreEqual(2, result.TotalJobs);
        }
    }
}
