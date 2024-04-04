using JobLink.Core.Contracts;
using JobLink.Infrastructure.Data.Models;

namespace JobLink.Core.Models.Company
{
    public class CompanyServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
