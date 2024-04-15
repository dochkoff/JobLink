using JobLink.Core.Models.Job;

namespace JobLink.Core.Contracts
{
    public interface IApplicantService
    {
        Task<bool> ApplicantExistsByIdAsync(string userId);

        Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);

        Task CreateApplicantAsync(string userId, string phoneNumber, string resumeURL);

        Task RemoveApplicantAsync(string userId);

        Task<int?> GetApplicantIdAsync(string userId);

        Task<IEnumerable<JobServiceModel>> AllJobApplicationsByUserIdAsync(string userId);
    }
}
