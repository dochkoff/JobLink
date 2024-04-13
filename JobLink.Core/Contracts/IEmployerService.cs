using JobLink.Core.Models.Company;
using JobLink.Core.Models.Job;

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
    }
}
