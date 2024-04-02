using JobLink.Core.Contracts;
using JobLink.Core.Enumerations;
using JobLink.Core.Models.Home;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Core.Services
{
    public class JobService : IJobService
    {
        private readonly IRepository repository;

        public JobService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<JobQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            JobSorting sorting = JobSorting.Newest,
            int currentPage = 1,
            int jobsPerPage = 1)
        {
            var jobsToShow = repository.AllReadOnly<Job>();

            if (category != null)
            {
                jobsToShow = jobsToShow
                    .Where(j => j.JobCategory.Name == category);
            }

            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToLower();
                jobsToShow = jobsToShow
                    .Where(j => j.Title.ToLower().Contains(normalizedSearchTerm) ||
                                j.Location.ToLower().Contains(normalizedSearchTerm) ||
                                j.Description.ToLower().Contains(normalizedSearchTerm));
            }

            jobsToShow = sorting switch
            {
                JobSorting.Salary => jobsToShow
                    .OrderBy(j => j.Salary),
                JobSorting.NoApplicationFirst => jobsToShow
                    .OrderBy(h => h.EmployerId != null)
                    .ThenByDescending(j => j.Id),
                _ => jobsToShow
                    .OrderByDescending(j => j.Id)
            };

            var jobs = await jobsToShow
                .Skip((currentPage - 1) * jobsPerPage)
                .Take(jobsPerPage)
                .ProjectToJobServiceModel()
                .ToListAsync();

            int totalJobs = await jobsToShow.CountAsync();

            return new JobQueryServiceModel()
            {
                Jobs = jobs,
                TotalJobsCount = totalJobs
            };
        }

        public async Task<IEnumerable<JobCategoryServiceModel>> AllCategoriesAsync()
        {
            return await repository.AllReadOnly<JobCategory>()
                .Select(jc => new JobCategoryServiceModel()
                {
                    Id = jc.Id,
                    Name = jc.Name,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<string>> AllCategoriesNamesAsync()
        {
            return await repository.AllReadOnly<JobCategory>()
                .Select(jc => jc.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<JobServiceModel>> AllJobsByEmployerIdAsync(int employerId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.EmployerId == employerId)
                .ProjectToJobServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<JobServiceModel>> AllJobsByApplicantId(int applicantId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.Applicants.Any(a => a.Id == applicantId))
                .ProjectToJobServiceModel()
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await repository.AllReadOnly<JobCategory>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public Task<IEnumerable<JobIndexServiceModel>> LastestJobsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(JobFormModel model, int employerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobServiceModel>> AllJobsByApplicantId(string applicantId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<JobDetailsServiceModel> JobDetailsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(int jobId, JobFormModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasEmployerWithIdAsync(int jobId, string employerId)
        {
            throw new NotImplementedException();
        }

        public Task<JobFormModel?> GetJobFormModelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int jobId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAppliedAsync(int jobId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAppliedByApplicantWithIdAsync(int jobId, string applicantId)
        {
            throw new NotImplementedException();
        }

        public Task ApplyAsync(int id, string applicantId)
        {
            throw new NotImplementedException();
        }

        public Task CancelAsync(int jobId, string applicantId)
        {
            throw new NotImplementedException();
        }
    }
}
