using JobLink.Core.Contracts;
using JobLink.Core.Services;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Tests.UnitTests
{
    [TestFixture]
    public class ApplicantServiceTests : UnitTestsBase
    {
        private IRepository _repository;
        private IApplicantService _applicantService;

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Repository(_data);
            _applicantService = new ApplicantService(_repository);
        }

        [Test]
        public async Task CreateApplicantAsync_ShouldAddApplicantToDatabase()
        {
            // Arrange
            var userId = "1";
            var phoneNumber = "123456789";
            var resumeURL = "https://example.com/resume";
            var expectedApplicant = new Applicant { UserId = userId, PhoneNumber = phoneNumber, ResumeUrl = resumeURL };

            // Act
            await _applicantService.CreateApplicantAsync(userId, phoneNumber, resumeURL);

            // Assert
            var savedApplicant = await _data.Applicants.FirstOrDefaultAsync(a => a.UserId == userId);
            Assert.IsNotNull(savedApplicant);
            Assert.AreEqual(userId, savedApplicant.UserId);
            Assert.AreEqual(phoneNumber, savedApplicant.PhoneNumber);
            Assert.AreEqual(resumeURL, savedApplicant.ResumeUrl);
        }

        [Test]
        public async Task RemoveApplicantAsync_ShouldRemoveApplicantFromDatabase()
        {
            // Arrange
            var userId = "2";
            var applicant = new Applicant { 
                UserId = userId,
                PhoneNumber = "123456789",
                ResumeUrl = "https://example.com/resume",
            };
            _data.Applicants.Add(applicant);
            await _data.SaveChangesAsync();

            // Act
            await _applicantService.RemoveApplicantAsync(userId);

            // Assert
            var removedApplicant = await _data.Applicants.FirstOrDefaultAsync(a => a.UserId == userId);
            Assert.IsNull(removedApplicant);
        }

        [Test]
        public async Task ApplicantExistsByIdAsync_ApplicantShouldExist()
        {
            // Arrange
            var userId = "3";
            var applicant = new Applicant
            {
                UserId = userId,
                PhoneNumber = "123456789",
                ResumeUrl = "https://example.com/resume",
            };
            _data.Applicants.Add(applicant);
            await _data.SaveChangesAsync();

            // Act
            var result = await _applicantService.ApplicantExistsByIdAsync(userId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ApplicantExistsByIdAsync_ApplicantShouldNotExist()
        {
            // Arrange
            var userId = "4";

            // Act
            var result = await _applicantService.ApplicantExistsByIdAsync(userId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetApplicantIdAsync_WhenCalledWithValidUserId_ShouldReturnApplicantId()
        {
            // Arrange
            var userId = "5";
            var expectedId = 10;
            var applicant = new Applicant
            {
                Id = expectedId,
                UserId = userId,
                PhoneNumber = "123456789",
                ResumeUrl = "https://example.com/resume",
            };
            _data.Applicants.Add(applicant);
            await _data.SaveChangesAsync();

            // Act
            var result = await _applicantService.GetApplicantIdAsync(userId);

            // Assert
            Assert.That(result, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task GetApplicantIdAsync_WhenCalledWithInvalidUserId_ShouldReturnNull()
        {
            // Arrange
            var userId = "6";

            // Act
            var result = await _applicantService.GetApplicantIdAsync(userId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UserWithPhoneNumberExistsAsync_UserShouldExist()
        {
            // Arrange
            var phoneNumber = "123456789";
            var applicant = new Applicant
            {
                Id = 11,
                UserId = "7",
                PhoneNumber = phoneNumber,
                ResumeUrl = "https://example.com/resume",
            };
            _data.Applicants.Add(applicant);
            await _data.SaveChangesAsync();

            // Act
            var result = await _applicantService.UserWithPhoneNumberExistsAsync(phoneNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserWithPhoneNumberExistsAsync_UserShouldNotExist()
        {
            // Arrange
            var phoneNumber = "987654321";

            // Act
            var result = await _applicantService.UserWithPhoneNumberExistsAsync(phoneNumber);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AllJobApplicationsByUserIdAsync_ShouldReturnAllJobApplicationsByUserId()
        {
            // Arrange
            var userId = "3";
            
            // Act
            var result = await _applicantService.AllJobApplicationsByUserIdAsync(userId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));

            // Assert job 1 details
            var job = result.Where(j=> j.Id == 1).FirstOrDefault();
            Assert.That(job.Id, Is.EqualTo(1));
            Assert.That(job.Title, Is.EqualTo("Software Developer"));
            Assert.That(job.Location, Is.EqualTo("Remote"));
        }
    }
}
