namespace JobLink.Core.Contracts
{
    public interface IApplicantService
    {
        Task<bool> ApplicantExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task CreateAsync(string userId, string name, string phoneNumber, string resumeURL);

        Task RemoveApplicantAsync(string userId);

        Task<int?> GetApplicantIdAsync(string userId);
    }
}
