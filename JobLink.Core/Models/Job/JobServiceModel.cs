using JobLink.Core.Contracts;
using System.ComponentModel.DataAnnotations;
using static JobLink.Core.Constants.MessageConstants;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Core.Models.Job
{
    public class JobServiceModel : IJobModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(JobTitleMaxLength,
            MinimumLength = JobTitleMinLength,
            ErrorMessage = LengthMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(LocationMaxLength,
            MinimumLength = LocationMinLength,
            ErrorMessage = LengthMessage)]
        public string Location { get; set; } = string.Empty;

        [Range(typeof(decimal),
            SalaryMinimum,
            SalaryMaximum,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = "Monthly salary must be a positive number and less than {2} leva")]
        [Display(Name = "Monthly salary")]
        public decimal Salary { get; set; }

        [Display(Name = "Is Applied")]
        public bool IsApplied { get; set; }
    }
}
