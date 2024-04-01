using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Infrastructure.Data.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Comment("Employer of a job")]
    public class Employer
    {
        [Key]
        [Comment("Employer identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(EmployerCompanyNameMaxLength)]
        [Comment("Employer's company name")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(LocationMaxLength)]
        [Comment("Company address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Comment("Employer's phone")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(EmployerWebsiteMaxLength)]
        [Comment("Employer's website")]
        public string Website { get; set; } = string.Empty;

        [MaxLength(UrlMaxLength)]
        [Comment("Employer's logo")]
        public string LogoUrl { get; set; } = string.Empty;

        [Required]
        [Comment("User Identifier")]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
