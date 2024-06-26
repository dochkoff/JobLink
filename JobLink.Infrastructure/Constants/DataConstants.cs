﻿namespace JobLink.Infrastructure.Constants
{
    public static class DataConstants
    {
        //Common

        public const int PhoneMaxLength = 15;

        public const int PhoneMinLength = 7;

        public const int LocationMaxLength = 150;

        public const int LocationMinLength = 5;

        public const int UrlMaxLength = 500;

        public const int UrlMinLength = 5;

        //Job
        public const int JobTitleMaxLength = 50;

        public const int JobTitleMinLength = 10;

        public const int JobDescriptionMaxLength = 1000;

        public const int JobDescriptionMinLength = 50;

        public const string SalaryMinimum = "0";

        public const string SalaryMaximum = "1000000";

        public const int JobCategoryNameMaxLength = 20;

        //Company
        public const int CompanyNameMaxLength = 30;

        public const int CompanyNameMinLength = 2;

        public const int CompanyWebsiteMaxLength = 50;

        public const int CompanyWebsiteMinLength = 5;

        public const int CompanyIdMinLength = 1;

        public const int CompanyIdMaxLength = 36;

        //AccountHolder

        public const int UserFirstNameMaxLength = 20;

        public const int UserFirstNameMinLength = 1;

        public const int UserLastNameMaxLength = 50;

        public const int UserLastNameMinLength = 5;
    }
}
