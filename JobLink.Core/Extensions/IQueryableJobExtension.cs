using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Models;

namespace System.Linq
{
    public static class IQueryableJobExtension
    {
        public static IQueryable<JobServiceModel> ProjectToJobServiceModel(this IQueryable<Job> jobs)
        {
            return jobs
                .Select(j => new JobServiceModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Location = j.Location,
                    Salary = j.Salary,
                    CompanyLogoURL = j.Employer.Company.LogoUrl,
                    IsApplied = j.EmployerId != null,
                });
        }
    }
}
