using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Infrastructure.Data.Models
{
    [Comment("A job posting")]
    public class Job
    {
        [Key]
        [Comment("Job identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(JobTitleMaxLength)]
        [Comment("Job title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(JobDescriptionMaxLength)]
        [Comment("Job description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(LocationMaxLength)]
        [Comment("Job location")]
        public string Location { get; set; } = string.Empty;

        [Comment("Monthly salary")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Required]
        [Comment("Category identifier")]
        public int CategoryId { get; set; }

        [Required]
        [Comment("Employer identifier")]
        public int EmployerId { get; set; }

        [Comment("A list with applications for the job")]
        public List<Application> Applications { get; set; } = new List<Application>();

        [ForeignKey(nameof(CategoryId))]
        public JobCategory JobCategory { get; set; } = null!;

        [ForeignKey(nameof(EmployerId))]
        public Employer Employer { get; set; } = null!;

    }
}
