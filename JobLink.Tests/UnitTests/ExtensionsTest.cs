using JobLink.Core.Contracts;
using JobLink.Core.Extensions;

namespace JobLink.Tests.UnitTests
{
    public class ExtensionsTest
    {
        [Test]
        public void GetJobInformation_ShouldReturnExpectedInfo()
        {
            // Arrange
            string expected = "Software-Developer-Remote";
            IJobModel job = new TestJobModel("Software Developer", "Remote");

            // Act
            string result = job.GetJobInformation();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetCompanyInformation_ShouldReturnExpectedInfo()
        {
            // Arrange
            string expected = "JobLink-Stara-Zagora";
            ICompanyModel company = new TestCompanyModel("JobLink", "Stara Zagora");

            // Act
            string result = company.GetCompanyInformation();

            // Assert
            Assert.AreEqual(expected, result);
        }

        // Implementations of IJobModel and ICompanyModel for testing purposes
        public class TestJobModel : IJobModel
        {
            public string Title { get; set; }
            public string Location { get; set; }

            public TestJobModel(string title, string location)
            {
                Title = title;
                Location = location;
            }
        }

        public class TestCompanyModel : ICompanyModel
        {
            public string Name { get; set; }
            public string Address { get; set; }

            public TestCompanyModel(string name, string address)
            {
                Name = name;
                Address = address;
            }
        }
    }
}
