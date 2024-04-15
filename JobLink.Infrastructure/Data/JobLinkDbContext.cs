using JobLink.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static JobLink.Infrastructure.Constants.CustomClaims;

namespace JobLink.Infrastructure.Data
{
    public class JobLinkDbContext : IdentityDbContext
    {
        //Properties for seeding data
        private AccountHolder AdministratorUser { get; set; }

        private AccountHolder EmployerUser { get; set; }

        private AccountHolder ApplicantUser { get; set; }

        private AccountHolder NewUser { get; set; }

        public IdentityUserClaim<string> AdministratorUserClaim { get; set; }

        public IdentityUserClaim<string> EmployerUserClaim { get; set; }

        public IdentityUserClaim<string> ApplicantUserClaim { get; set; }

        public IdentityUserClaim<string> NewUserClaim { get; set; }

        public IdentityRole AdministratorRole { get; set; }

        public IdentityUserRole<string> AdministratorUserRole { get; set; }

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
            .OnDelete(DeleteBehavior.NoAction);

            //Seed data
            SeedUsers();
            builder.Entity<AccountHolder>()
                .HasData(AdministratorUser,
                         EmployerUser,
                         ApplicantUser,
                         NewUser);

            builder.Entity<IdentityUserClaim<string>>()
                .HasData(AdministratorUserClaim,
                         EmployerUserClaim,
                         ApplicantUserClaim,
                         NewUserClaim);

            SeedRoles();
            builder.Entity<IdentityRole>()
                .HasData(AdministratorRole);

            builder.Entity<IdentityUserRole<string>>()
                .HasData(AdministratorUserRole);

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
            var hasher = new PasswordHasher<AccountHolder>();

            // Administrator
            AdministratorUser = new AccountHolder()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Strict",
                LastName = "Admin",
                UserName = "admin@joblink.com",
                NormalizedUserName = "ADMIN@JOBLINK.COM",
                Email = "admin@joblink.com",
                NormalizedEmail = "ADMIN@JOBLINK.COM"
            };

            AdministratorUserClaim = new IdentityUserClaim<string>()
            {
                Id = 1,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Strict Admin",
                UserId = AdministratorUser.Id
            };

            AdministratorUser.PasswordHash = hasher.HashPassword(AdministratorUser, "1qaz!QAZ");

            // Employer
            EmployerUser = new AccountHolder()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Stamo",
                LastName = "Blagodarya",
                UserName = "sirmarecruit@sirma.com",
                NormalizedUserName = "SIRMARECRUIT@SIRMA.COM",
                Email = "sirmarecruit@sirma.com",
                NormalizedEmail = "SIRMARECRUIT@SIRMA.COM"
            };

            EmployerUserClaim = new IdentityUserClaim<string>()
            {
                Id = 2,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Stamo Blagodarya",
                UserId = EmployerUser.Id
            };

            EmployerUser.PasswordHash = hasher.HashPassword(EmployerUser, "sirmarecruit123");

            // Applicant
            ApplicantUser = new AccountHolder()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Pavel",
                LastName = "Dochkov",
                UserName = "needajob@abv.bg",
                NormalizedUserName = "NEEDAJOB@ABV.BG",
                Email = "needajob@abv.bg",
                NormalizedEmail = "NEEDAJOB@ABV.BG"
            };

            ApplicantUserClaim = new IdentityUserClaim<string>()
            {
                Id = 3,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Pavel Dochkov",
                UserId = ApplicantUser.Id
            };

            ApplicantUser.PasswordHash = hasher.HashPassword(ApplicantUser, "needajob123");

            // New User
            NewUser = new AccountHolder()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Stefan",
                LastName = "Gorchev",
                UserName = "newuser@gmail.com",
                NormalizedUserName = "NEWUSER@GMAIL.COM",
                Email = "newuser@gmail.com",
                NormalizedEmail = "NEWUSER@GMAIL.COM"
            };

            NewUserClaim = new IdentityUserClaim<string>()
            {
                Id = 4,
                ClaimType = UserFullNameClaim,
                ClaimValue = "Stefan Gorchev",
                UserId = NewUser.Id
            };

            NewUser.PasswordHash = hasher.HashPassword(NewUser, "guest123");
        }
        
        private void SeedRoles()
        {
            AdministratorRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            };

            AdministratorUserRole = new IdentityUserRole<string>()
            {
                UserId = AdministratorUser.Id,
                RoleId = AdministratorRole.Id
            };
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
                LogoUrl = "/images/logos/logo-sirma.jpg",
                IsApproved = true

            };

            DraftKings = new Company
            {
                Id = Guid.NewGuid(),
                Name = "DraftKings",
                Address = "Boston, MA",
                PhoneNumber = "+16175551212",
                Website = "https://draftkings.com",
                LogoUrl = "/images/logos/logo-draftkings.png",
                IsApproved = true
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
                PhoneNumber = "+359886509188",
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
