using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static JobLink.Infrastructure.Constants.DataConstants;

namespace JobLink.Infrastructure.Data.Models
{
    [Comment("Job category")]
    public class JobCategory
    {
        [Key]
        [Comment("Category Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Comment("Category name")]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Job> Jobs { get; set; } = new List<Job>();
    }
}
