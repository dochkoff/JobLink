using JobLink.Core.Contracts;
using JobLink.Core.Models.Job;
using JobLink.Core.Services;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Tests.UnitTests
{
    public class JobServiceClass : UnitTestsBase
    {
        private IJobService _jobService;
        private IRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Repository(_data);
            _jobService = new JobService(_repository);
        }

        [Test]
        public async Task T01AllCategoriesAsync_ShouldReturnCategories()
        {
            // Arrange
            var categoriesCount = 3;

            // Act
            var categories = await _jobService.AllCategoriesAsync();

            // Assert
            Assert.AreEqual(categoriesCount, categories.Count());
            Assert.AreEqual("Software Development", categories.First().Name);
        }

        [Test]
        public async Task T02AllCategoriesNamesAsync_ShouldReturnCategoriesNames()
        {
            // Arrange
            var categoriesCount = 3;

            // Act
            var categories = await _jobService.AllCategoriesNamesAsync();

            // Assert
            Assert.AreEqual(categoriesCount, categories.Count());
            Assert.AreEqual("Software Development", categories.First());
            Assert.AreEqual("Sales", categories.Skip(1).First());
            Assert.AreEqual("Finance", categories.Last());
        }

        [Test]
        public async Task T03CategoryExistsAsync_ShouldConfirmCategoryExists()
        {
            // Arrange
            var categoryId = 1;

            // Act
            var result = await _jobService.CategoryExistsAsync(categoryId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task T04ExistsAsync_ShouldConfirnExistingOfAJob()
        {
            // Arrange
            var jobId = 1;

            // Act
            var result = await _jobService.ExistsAsync(jobId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task T05HasEmployerWithIdAsync_ShouldReturnIsEmployerIsFromTheCompany()
        {
            // Arrange
            var employerId = "2";
            var jobId = 1;

            // Act
            var result = await _jobService.HasEmployerWithIdAsync(jobId, employerId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task T06JobDetailsByIdAsync_ShouldReturnJobDetails()
        {
            // Arrange
            var jobId = 1;

            // Act
            var job = await _jobService.JobDetailsByIdAsync(jobId);

            // Assert
            Assert.AreEqual("Software Developer", job.Title);
            Assert.AreEqual("Develop software with ASP.NET Core with C#", job.Description);
            Assert.AreEqual("Remote", job.Location);
            Assert.AreEqual(5000M, job.Salary);;
        }

        [Test]
        public async Task T07LastestJobsAsync_ShouldReturnLastThreeJobs()
        {
            // Arrange
            var jobsCount = 2;

            // Act
            var jobs = await _jobService.LatestJobsAsync();

            // Assert
            Assert.AreEqual(jobsCount, jobs.Count());
            Assert.AreEqual("Sales Representative", jobs.First().Title);
        }

        [Test]
        public async Task T08GetJobFormModelByIdAsync_ShouldReturnJobFormModel()
        {
            // Arrange
            var jobId = 1;

            // Act
            var job = await _jobService.GetJobFormModelByIdAsync(jobId);

            // Assert
            Assert.AreEqual("Software Developer", job.Title);
            Assert.AreEqual("Develop software with ASP.NET Core with C#", job.Description);
            Assert.AreEqual("Remote", job.Location);
            Assert.AreEqual(5000M, job.Salary);
        }

        [Test]
        public async Task T09IsAppliedByUserWithIdAsync_ShouldReturnIsApplied()
        {
            // Arrange
            var jobId = 1;
            var userId = "3";

            // Act
            var result = await _jobService.IsAppliedByUserWithIdAsync(jobId, userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task T10IsAppliedByUserWithIdAsync_ShouldReturnIsUserApplied()
        {
            // Arrange
            var jobId = 1;
            var userId = "3";

            // Act
            var result = await _jobService.IsAppliedByUserWithIdAsync(jobId, userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task T11CancelAsync_ShouldCamcelUserForAJob()
        {
            // Arrange
            var jobId = 1;
            var userId = "3";

            // Act
            await _jobService.CancelAsync(jobId, userId);

            // Assert
            Assert.AreEqual(0, _data.Applications.Count());
        }

        [Test]
        public async Task T12ApplyAsync_ShouldApplyUserForAJob()
        {
            // Arrange
            var jobId = 1;
            var userId = "3";

            // Act
            await _jobService.ApplyAsync(jobId, userId);

            // Assert
            Assert.AreEqual(1, _data.Applications.Count());
            Assert.AreEqual(1, _data.Applications.Last().JobId);
            Assert.AreEqual(1, _data.Applications.Last().ApplicantId);

        }

        [Test]
        public async Task T13AddJobAsync_ShouldAddJob()
        {
            // Arrange
            var job = new JobFormModel
            {
                Title = "Java Developer",
                Description = "Develop with Java",
                Location = "Sofia",
                Salary = 5000M,
                CategoryId = 1,
            };


            // Act
            await _jobService.CreateJobAsync(job, 2);

            // Assert
            Assert.AreEqual(3, _data.Jobs.Count());
            Assert.AreEqual("Java Developer", _data.Jobs.Last().Title);

        }

        [Test]
        public async Task T14DeleteAsync_ShouldDeleteJob()
        {
            // Arrange
            var jobId = 3;

            // Act
            await _jobService.DeleteAsync(jobId);

            // Assert
            Assert.AreEqual(2, _data.Jobs.Count());
            Assert.AreEqual("Software Developer", _data.Jobs.First().Title);
        }

        [Test]
        public async Task T15EditAsync_ShouldEditJob()
        {
            // Arrange
            var job = new JobFormModel
            {
                Title = "Software Developer Edited",
                Description = "Develop software",
                Location = "Sofia",
                Salary = 5000M,
                CategoryId = 1,
            };

            // Act
            await _jobService.EditAsync(1, job);

            // Assert
            Assert.AreEqual("Software Developer Edited", _data.Jobs.First().Title);
        }

        
    }
}
