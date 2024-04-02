using JobLink.Core.Contracts;
using System.ComponentModel.DataAnnotations;
using static JobLink.Core.Constants.MessageConstants;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Core.Models.Job
{
    public class JobFormModel : IJobModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(JobTitleMaxLength,
            MinimumLength = JobTitleMinLength,
            ErrorMessage = LengthMessage)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(LocationMaxLength,
            MinimumLength = LocationMinLength,
            ErrorMessage = LengthMessage)]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(JobDescriptionMaxLength,
            MinimumLength = JobDescriptionMinLength,
            ErrorMessage = LengthMessage)]
        public string Description { get; set; } = null!;

        [Range(typeof(decimal),
            SalaryMinimum,
            SalaryMaximum,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = "Monthly salary must be a positive number and less than {2} leva")]
        [Display(Name = "Monthly salary")]
        public decimal Salary { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<JobCategoryServiceModel> Categories { get; set; } =
            new List<JobCategoryServiceModel>();
    }
}
