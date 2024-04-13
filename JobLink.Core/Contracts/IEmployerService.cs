using JobLink.Core.Models.Application;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Core.Contracts
{
    public interface IEmployerService
    {
        Task<bool> EmployerExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserHasApplicationsAsync(string userId);

        Task<bool> CompanyWithIdAndNameExistsAsync(string companyName, string companyId);

        Task CreateEmployerAsync(string userId, string phoneNumber, string companyId);

        Task<int?> GetEmployerIdAsync(string userId);

        Task<IEnumerable<JobServiceModel>> AllJobPostsByEmployerIdAsync(int employerId);

        Task<IEnumerable<ApplicationDetailsViewModel>> AllApplicationsByJobIdAsync(int jobId);
    }
}
