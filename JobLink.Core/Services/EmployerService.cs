﻿using JobLink.Core.Contracts;
using JobLink.Core.Models.Application;
using JobLink.Core.Models.Employer;
using JobLink.Core.Models.Job;
using JobLink.Infrastructure.Data.Common;
using JobLink.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;

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
                .Where(j => j.Employer.Company.IsApproved == true)
                .ProjectToJobServiceModel()
                .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationDetailsViewModel>> AllApplicationsByJobIdAsync(int jobId)
        {
            return await repository.AllReadOnly<Job>()
                .Where(j => j.Id == jobId)
                .SelectMany(j => j.Applications)
                .Select(a => new ApplicationDetailsViewModel()
                {
                    Id = a.Id,
                    JobTitle = a.Job.Title,
                    DateAndTime = a.DateAndTime.ToString("HH:mm dd/MM/yyyy"),
                    ApplicantName = $"{a.Applicant.User.FirstName} {a.Applicant.User.LastName}",
                    ApplicantPhoneNumber = a.Applicant.PhoneNumber,
                    ApplicantResumeUrl = a.Applicant.ResumeUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<EmployerServiceModel>> AllEmployersAsync()
        {
            return await repository.AllReadOnly<Employer>()
                .Select(e => new EmployerServiceModel()
                {
                    FullName = $"{e.User.FirstName} {e.User.LastName}",
                    PhoneNumber = e.PhoneNumber,
                    Email = e.User.Email,
                    CompanyName = e.Company.Name
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
