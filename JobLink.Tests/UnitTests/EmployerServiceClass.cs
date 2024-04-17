using JobLink.Core.Contracts;
using JobLink.Core.Models.Application;
using JobLink.Core.Services;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;


namespace JobLink.Tests.UnitTests
{
    [TestFixture]
    public class EmployerServiceClass : UnitTestsBase
    {
        private IEmployerService _employerService;
        private IApplicantService _applicantService;
        private IRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Repository(_data);
            _applicantService = new ApplicantService(_repository);
            _employerService = new EmployerService(_repository, _applicantService);
        }

        [Test]
        public async Task CreateEmployerAsync_WhenCalled_ShouldAddEmployerAndRemoveApplicant()
        {
            // Arrange
            string userId = "user1";
            string phoneNumber = "1234567890";
            string companyId = Guid.NewGuid().ToString();

            // Act
            await _employerService.CreateEmployerAsync(userId, phoneNumber, companyId);

            // Assert
            Assert.IsTrue(await _employerService.EmployerExistsByIdAsync(userId));
            Assert.IsFalse(await _applicantService.ApplicantExistsByIdAsync(userId));
            Assert.IsTrue(_repository.AllReadOnly<Employer>().Any(e => e.PhoneNumber == phoneNumber));
            Assert.IsTrue(_repository.AllReadOnly<Employer>().Any(e => e.CompanyId == new Guid(companyId)));
            Assert.IsTrue(_repository.AllReadOnly<Employer>().Any(e => e.UserId == userId));
        }

        [Test]
        public async Task EmployerExistsByIdAsync_WhenEmployerExists_ShouldReturnTrue()
        {
            // Arrange
            string userId = "1";

            // Act
            var result = await _employerService.EmployerExistsByIdAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task EmployerExistsByIdAsync_WhenEmployerDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            string userId = "nonexistentuser";

            // Act
            var result = await _employerService.EmployerExistsByIdAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetEmployerIdAsync_WhenCalledWithValidUserId_ShouldReturnEmployerId()
        {
            // Arrange
            string userId = "1";
            int expectedId = 1;

            // Act
            var result = await _employerService.GetEmployerIdAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task GetEmployerIdAsync_WhenCalledWithInvalidUserId_ShouldReturnNull()
        {
            // Arrange
            string userId = "nonexistentuser";

            // Act
            var result = await _employerService.GetEmployerIdAsync(userId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UserHasApplicationsAsync_WhenUserHasApplications_ShouldReturnTrue()
        {
            // Arrange
            string userId = "3";

            // Act
            var result = await _employerService.UserHasApplicationsAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserHasApplicationsAsync_WhenUserDoesNotHaveApplications_ShouldReturnFalse()
        {
            // Arrange
            string userId = "user2";

            // Act
            var result = await _employerService.UserHasApplicationsAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UserWithPhoneNumberExistsAsync_WhenPhoneNumberExists_ShouldReturnTrue()
        {
            // Arrange
            string phoneNumber = "1234567890";

            // Act
            var result = await _employerService.UserWithPhoneNumberExistsAsync(phoneNumber);
        }

        [Test]
        public async Task CompanyWithIdAndNameExistsAsync_WhenCompanyExists_ShouldReturnTrue()
        {
            // Arrange
            string companyName = "JobLink";
            string companyId = _data.Companies.First().Id.ToString().ToLower();

            // Act
            var result = await _employerService.CompanyWithIdAndNameExistsAsync(companyName, companyId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CompanyWithIdAndNameExistsAsync_WhenCompanyDoesNotExist_ShouldReturnFalse()
        {
            // Arrange
            string companyName = "NonexistentCompany";
            string companyId = Guid.NewGuid().ToString().ToLower();

            // Act
            var result = await _employerService.CompanyWithIdAndNameExistsAsync(companyName, companyId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AllApplicationsByJobIdAsync_WhenJobExists_ShouldReturnApplicationDetails()
        {
            // Arrange
            int jobId = _data.Jobs.First().Id;

            // Act
            var result = await _employerService.AllApplicationsByJobIdAsync(jobId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<ApplicationDetailsViewModel>>(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public async Task AllApplicationsByJobIdAsync_WhenJobDoesNotExist_ShouldReturnEmptyList()
        {
            // Arrange
            int nonExistentJobId = -1;

            // Act
            var result = await _employerService.AllApplicationsByJobIdAsync(nonExistentJobId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task AllJobPostsByEmployerIdAsync_ShouldReturnListOfJobServiceModels()
        {
            // Arrange
            var employerId = 2;
            int jobCount = 2;
            string jobTitle = "Software Developer";
            string jobLocation = "Remote";
            decimal jobSalary = 5000M;
            string jobLogoUrl = "/images/logos/logo-sirma.jpg";
            int jobApplicationsCount = 1;

            // Act
            var result = await _employerService.AllJobPostsByEmployerIdAsync(employerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(jobCount, result.Count());
            Assert.AreEqual(jobTitle, result.First().Title);
            Assert.AreEqual(jobLocation, result.First().Location);
            Assert.AreEqual(jobSalary, result.First().Salary);
            Assert.AreEqual(jobLogoUrl, result.First().CompanyLogoURL);
            Assert.AreEqual(jobApplicationsCount, result.First().ApplicationsCount);



        }
    }
}
