using JobLink.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Infrastructure.Data
{
    public class JobLinkDbContext : IdentityDbContext
    {
        //Properties for seeding data
        private IdentityUser EmployerUser { get; set; }

        private IdentityUser ApplicantUser { get; set; }

        private IdentityUser GuestUser { get; set; }

        private Employer Employer { get; set; }

        private Applicant Applicant { get; set; }

        private Company Sirma { get; set; }

        private Company DraftKings { get; set; }

        private JobCategory SoftDevJobCategory { get; set; }

        private JobCategory SalesJobCategory { get; set; }

        private JobCategory FinanceJobCategory { get; set; }

        private Job SoftDevJob { get; set; }

        private Job SalesJob { get; set; }

        private Job FinanceJob { get; set; }

        private Application SoftDevApplication { get; set; }

        private Application SalesApplication { get; set; }

        
        public JobLinkDbContext(DbContextOptions<JobLinkDbContext> options)
            : base(options)
        {
        }

        //DbSets
        public DbSet<Applicant> Applicants { get; set; } = null!;

        public DbSet<Application> Applications { get; set; } = null!;

        public DbSet<Company> Companies { get; set; } = null!;

        public DbSet<Employer> Employers { get; set; } = null!;

        public DbSet<Job> Jobs { get; set; } = null!;

        public DbSet<JobCategory> JobCategories { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Job>()
                .HasOne(j => j.JobCategory)
                .WithMany(jc => jc.Jobs)
                .HasForeignKey(j => j.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Job>()
                .HasOne(j => j.Employer)
                .WithMany(e => e.Jobs)
                .HasForeignKey(j => j.EmployerId)
            .OnDelete(DeleteBehavior.Restrict);

            //Seed data
            SeedUsers();
            builder.Entity<IdentityUser>()
                .HasData(EmployerUser,
                         ApplicantUser,
                         GuestUser);

            SeedCompanies();
            builder.Entity<Company>()
                .HasData(Sirma,
                        DraftKings);

            SeedEmployers();
            builder.Entity<Employer>()
                .HasData(Employer);

            SeedApplicants();
            builder.Entity<Applicant>()
                .HasData(Applicant);

            SeedJobCategories();
            builder.Entity<JobCategory>()
                .HasData(SoftDevJobCategory,
                         SalesJobCategory,
                         FinanceJobCategory);

            SeedJobs();
            builder.Entity<Job>()
                .HasData(SoftDevJob,
                         SalesJob,
                         FinanceJob);

            SeedApplications();
            builder.Entity<Application>()
                .HasData(SoftDevApplication,
                         SalesApplication);

            base.OnModelCreating(builder);
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            // Employer
            EmployerUser = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "sirmarecruit@sirma.com",
                NormalizedUserName = "sirmarecruit@sirma.com",
                Email = "sirmarecruit@sirma.com",
                NormalizedEmail = "sirmarecruit@sirma.com"
            };

            EmployerUser.PasswordHash = hasher.HashPassword(EmployerUser, "sirmarecruit123");

            // Applicant
            ApplicantUser = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "needajob@abv.bg",
                NormalizedUserName = "needajob@abv.bg",
                Email = "needajob@abv.bg",
                NormalizedEmail = "needajob@abv.bg"
            };

            ApplicantUser.PasswordHash = hasher.HashPassword(ApplicantUser, "needajob123");

            // Guest
            GuestUser = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "guest@gmail.com",
                NormalizedUserName = "guest@gmail.com",
                Email = "guest@gmail.com",
                NormalizedEmail = "guest@gmail.com"
            };

            GuestUser.PasswordHash = hasher.HashPassword(GuestUser, "guest123");
        }

        private void SeedCompanies()
        {
            Sirma = new Company
            {
                Id = Guid.NewGuid(),
                Name = "Sirma Solutions",
                Address = "Sofia, Bulgaria",
                PhoneNumber = "+359 2 976 8310",
                Website = "https://sirma.com",
                LogoUrl = "logo-sirma.jpg",
                IsActive = true

            };

            DraftKings = new Company
            {
                Id = Guid.NewGuid(),
                Name = "DraftKings",
                Address = "Boston, MA",
                PhoneNumber = "+16175551212",
                Website = "https://draftkings.com",
                LogoUrl = "logo-draftkings.png",
                IsActive = true
            };
        }

        private void SeedEmployers()
        {
            Employer = new Employer
            {
                Id = 1,
                PhoneNumber = "+359880000000",
                UserId = EmployerUser.Id,
                CompanyId = Sirma.Id
            };
        }

        private void SeedApplicants()
        {
            Applicant = new Applicant
            {
                Id = 1,
                Name = "Pavel Dochkov",
                PhoneNumber = "+359887654321",
                ResumeUrl = "https://drive.google.com/file/d/1UeDWXN60iwk-iVav4_Wj0aekCdWn2BuE/view?usp=sharing",
                UserId = ApplicantUser.Id
            };
        }

        private void SeedJobCategories()
        {
            SoftDevJobCategory = new JobCategory
            {
                Id = 1,
                Name = "Software Development"
            };

            SalesJobCategory = new JobCategory
            {
                Id = 2,
                Name = "Sales"
            };

            FinanceJobCategory = new JobCategory
            {
                Id = 3,
                Name = "Finance"
            };
        }

        private void SeedJobs()
        {
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

            FinanceJob = new Job
            {
                Id = 3,
                Title = "Accountant",
                Description = "Do accounting for Sirma Solutions staff",
                Location = "Kazanlak, Bulgaria",
                Salary = 2500M,
                CategoryId = FinanceJobCategory.Id,
                EmployerId = Employer.Id
            };
        }

        private void SeedApplications()
        {
            SoftDevApplication = new Application
            {
                Id = 1,
                DateAndTime = DateTime.Now,
                ApplicantId = Applicant.Id,
                JobId = SoftDevJob.Id
            };

            SalesApplication = new Application
            {
                Id = 2,
                DateAndTime = DateTime.Now,
                ApplicantId = Applicant.Id,
                JobId = SalesJob.Id
            };
        }
    }
}
