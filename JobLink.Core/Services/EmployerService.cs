using JobLink.Core.Contracts;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Core.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IRepository repository;
        private readonly IApplicantService applicantService;

        public EmployerService(IRepository _repository,
            IApplicantService _applicantService)
        {
            repository = _repository;
            applicantService = _applicantService;
        }

        public async Task CreateEmployerAsync(string userId, string phoneNumber, string companyId)
        {
            await repository.AddAsync(new Employer()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
                CompanyId = new Guid(companyId)
            });

            if ((await applicantService.ApplicantExistsByIdAsync(userId)) == true)
            {
                await applicantService.RemoveApplicantAsync(userId);
            }

            await repository.SaveChangesAsync();
        }

        public async Task<bool> EmployerExistsByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Employer>()
                .AnyAsync(e => e.UserId == userId);
        }

        public async Task<int?> GetEmployerIdAsync(string userId)
        {
            return (await repository.AllReadOnly<Employer>()
                .FirstOrDefaultAsync(e => e.UserId == userId))?.Id;
        }

        public async Task<bool> UserHasApplicationsAsync(string userId)
        {
            return await repository.AllReadOnly<Application>()
                .AnyAsync(a => a.Applicant.UserId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Employer>()
                .AnyAsync(e => e.PhoneNumber == phoneNumber);
        }

        public async Task<bool> CompanyWithIdAndNameExistsAsync(string companyName, string companyId)
        {
            return await repository.AllReadOnly<Company>()
                .AnyAsync(c => c.Name == companyName && c.Id.ToString().ToLower() == companyId.ToLower());
        }

        public async Task<IEnumerable<JobServiceModel>> AllJobPostsByEmployerIdAsync(int employerId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.EmployerId == employerId)
                .ProjectToJobServiceModel()
                .ToListAsync();
        }
    }
}
