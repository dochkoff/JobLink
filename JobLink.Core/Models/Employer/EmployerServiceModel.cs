using System.ComponentModel.DataAnnotations;

namespace JobLink.Core.Models.Employer
{
    public class EmployerServiceModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string CompanyName { get; set; } = null!;
    }
}
