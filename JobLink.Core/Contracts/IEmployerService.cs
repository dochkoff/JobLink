using JobLink.Core.Models.Company;

namespace JobLink.Core.Contracts
{
    public interface IEmployerService
    {
        Task<bool> ExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserHasApplicationsAsync(string userId);

        Task CreateAsync(string userId, string phoneNumber, int companyId);

        Task<int?> GetEmployerIdAsync(string userId);
    }
}
