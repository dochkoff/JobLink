using JobLink.Core.Contracts;
using JobLink.Core.Enumerations;
using JobLink.Core.Exceptions;
using JobLink.Core.Models.Home;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using static JobLink.Core.Constants.MessageConstants;

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

        public async Task<IEnumerable<JobServiceModel>> AllJobPostsByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.Employer.UserId == userId)
                .ProjectToJobServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<JobServiceModel>> AllJobApplicationsByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.Applications.Any(a => a.Applicant.UserId == userId))
                .ProjectToJobServiceModel()
                .ToListAsync();
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await repository.AllReadOnly<JobCategory>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<int> CreateAsync(JobFormModel model, int employerId)
        {
            Job job = new Job()
            {
                Title = model.Title,
                Description = model.Description,
                Location = model.Location,
                Salary = model.Salary,
                CategoryId = model.CategoryId,
                EmployerId = employerId
            };

            await repository.AddAsync(job);
            await repository.SaveChangesAsync();

            return job.Id;
        }

        public async Task DeleteAsync(int jobId)
        {
            await repository.DeleteAsync<Job>(jobId);
            await repository.SaveChangesAsync();
        }

        public async Task EditAsync(int jobId, JobFormModel model)
        {
            var job = await repository.GetByIdAsync<Job>(jobId);

            if (job != null)
            {
                job.Title = model.Title;
                job.Description = model.Description;
                job.Location = model.Location;
                job.Salary = model.Salary;
                job.CategoryId = model.CategoryId;

                await repository.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int jobId)
        {
            return await repository.AllReadOnly<Job>()
                .AnyAsync(j => j.Id == jobId);
        }

        public async Task<JobFormModel?> GetJobFormModelByIdAsync(int jobId)
        {
            var job = await repository.AllReadOnly<Job>()
                .Where(j => j.Id == jobId)
                .Select(j => new JobFormModel()
                {
                    Title = j.Title,
                    Description = j.Description,
                    Location = j.Location,
                    Salary = j.Salary,
                    CategoryId = j.CategoryId
                })
                .FirstOrDefaultAsync();

            if (job != null)
            {
                job.Categories = await AllCategoriesAsync();
            }

            return job;
        }

        public async Task<bool> HasEmployerWithIdAsync(int jobId, string userId)
        {
            return await repository.AllReadOnly<Job>()
                .AnyAsync(j => j.Id == jobId && j.Employer.UserId == userId);
        }

        public async Task<JobDetailsServiceModel> JobDetailsByIdAsync(int jobId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.Id == jobId)
                .Select(j => new JobDetailsServiceModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Description = j.Description,
                    Location = j.Location,
                    Salary = j.Salary,
                    Employer = new Models.Employer.EmployerServiceModel()
                    {
                        Email = j.Employer.User.Email,
                        PhoneNumber = j.Employer.PhoneNumber,
                        CompanyName = j.Employer.Company.Name,
                    },
                    Category = j.JobCategory.Name,
                    CompanyLogoURL = j.Employer.Company.LogoUrl,
                    ApplicationsCount = j.Applications.Count()
                })
                .FirstAsync();
        }

        //public async Task<bool> IsAppliedAsync(int jobId)
        //{
        //    bool result = false;
        //    var job = await repository.GetByIdAsync<Job>(jobId);

        //    if (job != null)
        //    {
        //        result = job.Applicants != null;
        //    }

        //    return result;
        //}

        public async Task<bool> IsAppliedByUserWithIdAsync(int jobId, string userId)
        {
            bool result = false;

            var application = await repository.AllReadOnly<Application>()
                .FirstOrDefaultAsync(a => a.Applicant.UserId == userId && a.JobId == jobId);

            if (application != null)
            {
                result = true;
            }

            return result;
        }

        public async Task<IEnumerable<JobIndexServiceModel>> LatestJobsAsync()
        {
            return await repository
                .AllReadOnly<Job>()
                .OrderByDescending(j => j.Id)
                .Take(3)
                .Select(j => new JobIndexServiceModel()
                {
                    Id = j.Id,
                    Title = j.Title,
                    Employer = j.Employer.Company.Name,
                    Location = j.Location,
                    ImageUrl = j.Employer.Company.LogoUrl
                })
                .ToListAsync();
        }

        public async Task CancelAsync(int jobId, string applicantId)
        {
            var job = await repository.GetByIdAsync<Job>(jobId);
            var applicant = await repository.AllReadOnly<Applicant>()
                .FirstAsync(a => a.UserId == applicantId);

            var application = await repository.AllReadOnly<Application>()
                        .FirstAsync(a => a.Applicant.UserId == applicantId && a.JobId == jobId);

            if (application !=null)
            {
                await repository.DeleteAsync<Application>(application.Id);
                //job.Applications.Remove(application);
                //applicant.Applications.Remove(application);
                await repository.SaveChangesAsync();
            }
            else
            {
                throw new UnauthorizedActionException(NotAnApplicant);
            }
        }

        public async Task ApplyAsync(int jobId, string applicantId)
        {
            var job = await repository.GetByIdAsync<Job>(jobId);

            var applicant = await repository.AllReadOnly<Applicant>()
                .FirstAsync(a => a.UserId == applicantId);

            if (job != null && applicant != null)
            {
                if (job.Applications.Any(a => a.Applicant.UserId == applicantId))
                {
                    throw new ApplicantAlreadyExistException(AlreadyApplied);
                }

                var application = new Application()
                {
                    JobId = jobId,
                    ApplicantId = applicant.Id
                };

                await repository.AddAsync(application);
                //job.Applications.Add(application);
                //applicant.Applications.Add(application);

                await repository.SaveChangesAsync();
            }
        }
    }
}
