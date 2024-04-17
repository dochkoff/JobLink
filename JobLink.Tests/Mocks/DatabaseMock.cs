using JobLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Tests.Mocks
{
    public static class DatabaseMock
    {
        public static JobLinkDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<JobLinkDbContext>()
                    .UseInMemoryDatabase("JobLinkInMemoryDb"
                    + DateTime.Now.Ticks.ToString())
                    .Options;

                return new JobLinkDbContext(dbContextOptions, false);
            }
        }
    }
}
