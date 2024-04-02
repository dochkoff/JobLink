using System.ComponentModel.DataAnnotations;

namespace JobLink.Core.Models.Employer
{
    public class EmployerServiceModel
    {
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
