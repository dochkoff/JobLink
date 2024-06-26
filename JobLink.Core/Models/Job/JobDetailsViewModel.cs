﻿using JobLink.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace JobLink.Core.Models.Job
{
    public class JobDetailsViewModel : IJobModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        [Display(Name = "Company Logo")]
        public string CompanyLogoURL { get; set; } = null!;

    }
}
