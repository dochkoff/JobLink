namespace JobLink.Core.Contracts
{
    public interface IApplicantService
    {
        Task<bool> ExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task<bool> UserIsEmployerAsync(string userId);

        Task CreateAsync(string userId, string name, string phoneNumber, string resumeURL);

        Task<int?> GetApplicantIdAsync(string userId);
    }
}
