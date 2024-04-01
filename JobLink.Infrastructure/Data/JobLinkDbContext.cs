using JobLink.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Infrastructure.Data
{
    public class JobLinkDbContext : IdentityDbContext
    {
        public JobLinkDbContext(DbContextOptions<JobLinkDbContext> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; } = null!;

        public DbSet<Application> Applications { get; set; } = null!;

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

            base.OnModelCreating(builder);
        }

    }
}
