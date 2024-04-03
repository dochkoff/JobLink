using JobLink.Core.Contracts;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JobLink.Core.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IRepository repository;

        public EmployerService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            await repository.AddAsync(new Employer()
            {
                UserId = userId,
                PhoneNumber = phoneNumber
            });

            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string userId)
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
            return await repository.AllReadOnly<Job>()
                .AnyAsync(j => j.Applicants.Any(a=>a.UserId == userId));
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Employer>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
