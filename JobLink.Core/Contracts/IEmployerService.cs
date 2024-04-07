using JobLink.Core.Models.Company;

namespace JobLink.Core.Contracts
{
    public interface IEmployerService
    {
        Task<bool> EmployerExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserHasApplicationsAsync(string userId);

        Task<bool> CompanyWithIdAndNameExistsAsync(string companyName, string companyId);

        Task CreateAsync(string userId, string phoneNumber, string companyId);

        Task<int?> GetEmployerIdAsync(string userId);
    }
}
