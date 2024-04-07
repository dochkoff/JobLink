using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Infrastructure.Data.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Comment("Company")]
    public class Company
    {
        [Key]
        [Comment("Company identifier")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CompanyNameMaxLength)]
        [Comment("Company name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(LocationMaxLength)]
        [Comment("Company address")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(PhoneMaxLength)]
        [Comment("Company's phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(CompanyWebsiteMaxLength)]
        [Comment("Company's website")]
        public string Website { get; set; } = string.Empty;

        [MaxLength(UrlMaxLength)]
        [Comment("Company's logo")]
        public string LogoUrl { get; set; } = string.Empty;
    }
}
