using JobLink.Core.Contracts;
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

        public async Task CreateAsync(string userId,string name, string phoneNumber, string resumeURL)
        {
            await repository.AddAsync(new Applicant()
            {
                UserId = userId,
                Name = name,
                PhoneNumber = phoneNumber,
                ResumeUrl = resumeURL
            });

            await repository.SaveChangesAsync();
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
    }
}
