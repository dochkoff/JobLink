using JobLink.Infrastructure.Data.Models;

namespace JobLink.Tests.UnitTests
{
    [TestFixture]
    public class IQueryableJobExtensionTests
    {
        [Test]
        public void ProjectToJobServiceModel_ShouldProjectJobEntitiesToJobServiceModel()
        {
            // Arrange
            var jobs = new List<Job>
            {
                new Job
                {
                    Id = 1,
                    Title = "Software Developer",
                    Location = "Remote",
                    Salary = 5000M,
                    Employer = new Employer
                    {
                        Company = new Company
                        {
                            LogoUrl = "/images/logos/company1.jpg"
                        }
                    },
                    Applications = new List<Application>
                    {
                        new Application(),
                        new Application(),
                        new Application()
                    }
                },
                new Job
                {
                    Id = 2,
                    Title = "Sales Representative",
                    Location = "New York",
                    Salary = 4000M,
                    Employer = new Employer
                    {
                        Company = new Company
                        {
                            LogoUrl = "/images/logos/company2.jpg"
                        }
                    },
                    Applications = new List<Application>
                    {
                        new Application(),
                        new Application()
                    }
                }
            }.AsQueryable();

            // Act
            var result = jobs.ProjectToJobServiceModel().ToList();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));

            // Assert job 1 details
            var job1 = result[0];
            Assert.That(job1.Id, Is.EqualTo(1));
            Assert.That(job1.Title, Is.EqualTo("Software Developer"));
            Assert.That(job1.Location, Is.EqualTo("Remote"));
            Assert.That(job1.Salary, Is.EqualTo(5000M));
            Assert.That(job1.CompanyLogoURL, Is.EqualTo("/images/logos/company1.jpg"));
            Assert.That(job1.ApplicationsCount, Is.EqualTo(3));

            // Assert job 2 details
            var job2 = result[1];
            Assert.That(job2.Id, Is.EqualTo(2));
            Assert.That(job2.Title, Is.EqualTo("Sales Representative"));
            Assert.That(job2.Location, Is.EqualTo("New York"));
            Assert.That(job2.Salary, Is.EqualTo(4000M));
            Assert.That(job2.CompanyLogoURL, Is.EqualTo("/images/logos/company2.jpg"));
            Assert.That(job2.ApplicationsCount, Is.EqualTo(2));
        }
    }
}
