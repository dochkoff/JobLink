using JobLink.Core.Enumerations;
using JobLink.Core.Models.Home;
using JobLink.Core.Models.Job;

namespace JobLink.Core.Contracts
{
    public interface IJobService
    {
        Task<IEnumerable<JobIndexServiceModel>> LatestJobsAsync();

        Task<IEnumerable<JobCategoryServiceModel>> AllCategoriesAsync();

        Task<bool> CategoryExistsAsync(int categoryId);

        Task<int> CreateAsync(JobFormModel model, int employerId);

        Task<JobQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            JobSorting sorting = JobSorting.Newest,
            int currentPage = 1,
            int jobsPerPage = 1);

        Task<IEnumerable<string>> AllCategoriesNamesAsync();

        Task<IEnumerable<JobServiceModel>> AllJobsByEmployerIdAsync(int employerId);

        Task<IEnumerable<JobServiceModel>> AllJobsByApplicantId(int applicantId);

        Task<bool> ExistsAsync(int id);

        Task<JobDetailsServiceModel> JobDetailsByIdAsync(int id);

        Task EditAsync(int jobId, JobFormModel model);

        Task<bool> HasEmployerWithIdAsync(int jobId, string employerId);

        Task<JobFormModel?> GetJobFormModelByIdAsync(int id);

        Task DeleteAsync(int jobId);

        Task<bool> IsAppliedAsync(int jobId);

        Task<bool> IsAppliedByApplicantWithIdAsync(int jobId, int applicantId);

        Task ApplyAsync(int id, int applicantId);

        Task LeaveAsync(int jobId, int applicantId);
    }
}
