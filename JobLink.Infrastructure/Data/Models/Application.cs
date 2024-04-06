using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobLink.Infrastructure.Data.Models
{
    [Comment("Application for a job")]
    public class Application
    {
        [Key]
        [Comment("Application Identifier")]
        public int Id { get; set; }

        [Required]
        [Comment("Job Identifier")]
        public int JobId { get; set; }

        [Required]
        [Comment("Applicant Identifier")]
        public int ApplicantId { get; set; }

        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; } = null!;

        [ForeignKey(nameof(ApplicantId))]
        public Applicant Applicant { get; set; } = null!;
    }
}
