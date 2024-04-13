using JobLink.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobLink.Core.Models.Application
{
    internal class ApplicationDetailsViewModel : IApplicationModel
    {
        public int Id { get; set;}

        public string DateAndTime { get; set;} = string.Empty;

        public string ApplicantName { get; set; } = string.Empty;

        public string ApplicantPhoneNumber { get; set; } = string.Empty;

        public string ApplicantResumeUrl { get; set; } = string.Empty;
    }
}
