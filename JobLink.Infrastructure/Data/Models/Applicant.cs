using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Infrastructure.Data.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Comment("Applicant for a job")]
    public class Applicant
    {
        [Key]
        [Comment("Applicant Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Comment("Agent's phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(UrlMaxLength)]
        [Comment("Applicant's resume")]
        public string ResumeUrl { get; set; } = string.Empty;

        [Required]
        [Comment("User Identifier")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public AccountHolder User { get; set; } = null!;

        [Comment("A list of user applications")]
        public List<Application> Applications { get; set; } = new List<Application>();
    }
}
