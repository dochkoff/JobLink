using JobLink.Core.Contracts;
using JobLink.Core.Models.Company;
using JobLink.Core.Services;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Tests.UnitTests
{
    public class CompanyServiceClass : UnitTestsBase
    {
        private ICompanyService _companyService;
        private IRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            _repository = new Repository(_data);
            _companyService = new CompanyService(_repository);
        }

        [Test]
        public async Task AllApprovedCompaniesAsync_ReturnsCorrectCompanies()
        {
            // Arrange
            var approvedCompanies = 2;

            // Act
            var result = await _companyService.AllApprovedCompaniesAsync();

            // Assert
            Assert.AreEqual(approvedCompanies, result.Companies.Count());
        }

        [Test]
        public async Task AllNonApprovedCompaniesAsync_ReturnsCorrectCompanies()
        {
            // Arrange
            var nonApprovedCompanies = 0;

            // Act
            var result = await _companyService.AllNonApprovedCompaniesAsync();

            // Assert
            Assert.AreEqual(nonApprovedCompanies, result.Companies.Count());
        }

        [Test]
        public async Task ApproveCompanyAsync_WhenCalled_ShouldApproveCompany()
        {
            // Arrange
            var companyId = "e22242f1-8818-424c-92b1-ab9deb1b7445";

            // Act
            await _companyService.ApproveCompanyAsync(companyId);

            // Assert
            Assert.IsTrue(await _companyService.CompanyExistsAsync(companyId));
            Assert.IsTrue(_repository.AllReadOnly<Company>().Any(c => c.IsApproved));
        }

        [Test]
        public async Task RejectCompanyAsync_WhenCalled_ShouldRejectCompany()
        {
            // Arrange
            var companyId = "e22242f1-8818-424c-92b1-ab9deb1b7445";

            // Act
            await _companyService.RejectCompanyAsync(companyId);

            // Assert
            Assert.IsTrue(await _companyService.CompanyExistsAsync(companyId));
            Assert.IsFalse(_repository.AllReadOnly<Company>().Any(c => c.IsApproved && c.Id.ToString() == companyId));
        }

        [Test]
        public async Task CompanyDetailsByIdAsync_WhenCalled_ShouldReturnCorrectCompanyDetails()
        {
            // Arrange
            var companyId = "e22242f1-8818-424c-92b1-ab9deb1b7445";

            // Act
            var result = await _companyService.CompanyDetailsByIdAsync(companyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(companyId, result.Id);
        }

        [Test]
        public async Task CompanyExistsAsync_WhenCompanyExists_ShouldReturnTrue()
        {
            // Arrange
            var companyId = "e22242f1-8818-424c-92b1-ab9deb1b7445";

            // Act
            var result = await _companyService.CompanyExistsAsync(companyId);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
