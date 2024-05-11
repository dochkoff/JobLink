using JobLink.Core.Contracts;
using JobLink.Core.Models.Applicant;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Core.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IRepository repository;

        public ApplicantService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateApplicantAsync(string userId, string phoneNumber, string resumeURL)
        {
            await repository.AddAsync(new Applicant()
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
                ResumeUrl = resumeURL
            });

            await repository.SaveChangesAsync();
        }

        public async Task RemoveApplicantAsync(string userId)
        {
            var applicant = await repository.All<Applicant>()
                .FirstOrDefaultAsync(a => a.UserId == userId);

            if (applicant != null)
            {
                await repository.DeleteAsync<Applicant>(applicant.Id);
                await repository.SaveChangesAsync();
            }            
        }

        public async Task<bool> ApplicantExistsByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Applicant>()
                .AnyAsync(e => e.UserId == userId);
        }

        public async Task<int?> GetApplicantIdAsync(string userId)
        {
            return (await repository.AllReadOnly<Applicant>()
                .FirstOrDefaultAsync(e => e.UserId == userId))?.Id;
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Applicant>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<JobServiceModel>> AllJobApplicationsByUserIdAsync(string userId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.Employer.Company.IsApproved == true)
                .Where(j => j.Applications
                        .Any(a => a.Applicant.UserId == userId))
                .ProjectToJobServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicantServiceModel>> AllApplicantsAsync()
        {
            return await repository.AllReadOnly<Applicant>()
                .Select(a => new ApplicantServiceModel()
                {
                    Name = $"{a.User.FirstName} {a.User.LastName}",
                    PhoneNumber = a.PhoneNumber,
                    Email = a.User.Email,
                    ResumeUrl = a.ResumeUrl
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
