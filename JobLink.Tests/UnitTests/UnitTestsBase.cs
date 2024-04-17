using JobLink.Infrastructure.Data;
using JobLink.Infrastructure.Data.Models;
using JobLink.Tests.Mocks;
using Microsoft.AspNetCore.Identity;

namespace JobLink.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected JobLinkDbContext _data;

        private AccountHolder AdministratorUser { get; set; }

        private AccountHolder EmployerUser { get; set; }

        private AccountHolder ApplicantUser { get; set; }

        public IdentityRole AdministratorRole { get; set; }

        public IdentityUserRole<string> AdministratorUserRole { get; set; }

        private Employer Employer { get; set; }

        private Employer AdminEmployer { get; set; }

        private Applicant Applicant { get; set; }

        private Company JobLink { get; set; }

        private Company Sirma { get; set; }

        private JobCategory SoftDevJobCategory { get; set; }

        private JobCategory SalesJobCategory { get; set; }

        private JobCategory FinanceJobCategory { get; set; }

        private Job SoftDevJob { get; set; }

        private Job SalesJob { get; set; }

        private Application SoftDevApplication { get; set; }

        [OneTimeSetUp]
        public void SetUpBase()
        {
            _data = DatabaseMock.Instance;
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Administrator
            AdministratorUser = new AccountHolder()
            {
                Id = "1",
                FirstName = "Strict",
                LastName = "Admin",
                UserName = "admin@joblink.com",
                NormalizedUserName = "ADMIN@JOBLINK.COM",
                Email = "admin@joblink.com",
                NormalizedEmail = "ADMIN@JOBLINK.COM"
            };
            _data.Users.Add(AdministratorUser);

            // Employer
            EmployerUser = new AccountHolder()
            {
                Id = "2",
                FirstName = "Stamo",
                LastName = "Blagodarya",
                UserName = "sirmarecruit@sirma.com",
                NormalizedUserName = "SIRMARECRUIT@SIRMA.COM",
                Email = "sirmarecruit@sirma.com",
                NormalizedEmail = "SIRMARECRUIT@SIRMA.COM"
            };
            _data.Users.Add(EmployerUser);

            // Applicant
            ApplicantUser = new AccountHolder()
            {
                Id = "3",
                FirstName = "Pavel",
                LastName = "Dochkov",
                UserName = "needajob@abv.bg",
                NormalizedUserName = "NEEDAJOB@ABV.BG",
                Email = "needajob@abv.bg",
                NormalizedEmail = "NEEDAJOB@ABV.BG"
            };
            _data.Users.Add(ApplicantUser);

            // Administrator Role
            AdministratorRole = new IdentityRole()
            {
                Id = "1",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            };
            _data.Roles.Add(AdministratorRole);

            AdministratorUserRole = new IdentityUserRole<string>()
            {
                UserId = AdministratorUser.Id,
                RoleId = AdministratorRole.Id
            };
            _data.UserRoles.Add(AdministratorUserRole);

            // Seed Companies
            JobLink = new Company
            {
                Id = new Guid("0d30595b-de23-4444-8851-1d75bd20f95a"),
                Name = "JobLink",
                Address = "Stara Zagora, Bulgaria",
                PhoneNumber = "+359 42 333 999",
                Website = "https://joblink.com",
                LogoUrl = "/images/logos/logo-joblink.jpg",
                IsApproved = true
            };
            _data.Companies.Add(JobLink);

            Sirma = new Company
            {
                Id = new Guid("e22242f1-8818-424c-92b1-ab9deb1b7445"),
                Name = "Sirma Solutions",
                Address = "Sofia, Bulgaria",
                PhoneNumber = "+359 2 976 8310",
                Website = "https://sirma.com",
                LogoUrl = "/images/logos/logo-sirma.jpg",
                IsApproved = true
            };
            _data.Companies.Add(Sirma);

            // Seed Employers
            AdminEmployer = new Employer
            {
                Id = 1,
                PhoneNumber = "+359880000000",
                UserId = AdministratorUser.Id,
                CompanyId = JobLink.Id
            };
            _data.Employers.Add(AdminEmployer);

            Employer = new Employer
            {
                Id = 2,
                PhoneNumber = "+359887654321",
                UserId = EmployerUser.Id,
                CompanyId = Sirma.Id
            };
            _data.Employers.Add(Employer);

            // Seed Applicants
            Applicant = new Applicant
            {
                Id = 1,
                PhoneNumber = "+359886509188",
                ResumeUrl = "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing",
                UserId = ApplicantUser.Id
            };
            _data.Applicants.Add(Applicant);

            // Seed Job Categories
            SoftDevJobCategory = new JobCategory
            {
                Id = 1,
                Name = "Software Development"
            };
            _data.JobCategories.Add(SoftDevJobCategory);

            SalesJobCategory = new JobCategory
            {
                Id = 2,
                Name = "Sales"
            };
            _data.JobCategories.Add(SalesJobCategory);

            FinanceJobCategory = new JobCategory
            {
                Id = 3,
                Name = "Finance"
            };
            _data.JobCategories.Add(FinanceJobCategory);
                
            // Seed Jobs
            SoftDevJob = new Job
            {
                Id = 1,
                Title = "Software Developer",
                Description = "Develop software with ASP.NET Core with C#",
                Location = "Remote",
                Salary = 5000M,
                CategoryId = SoftDevJobCategory.Id,
                EmployerId = Employer.Id
            };
            _data.Jobs.Add(SoftDevJob);

            SalesJob = new Job
            {
                Id = 2,
                Title = "Sales Representative",
                Description = "Sell our software platform internationaly",
                Location = "Sofia, Bulgaria",
                Salary = 3000M,
                CategoryId = SalesJobCategory.Id,
                EmployerId = Employer.Id
            };
            _data.Jobs.Add(SalesJob);

            // Seed Applications
            SoftDevApplication = new Application
            {
                Id = 1,
                DateAndTime = DateTime.Now,
                ApplicantId = Applicant.Id,
                JobId = SoftDevJob.Id
            };
            _data.Applications.Add(SoftDevApplication);

            _data.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownBase()
        {
            _data.Dispose();
        }
    }
}
