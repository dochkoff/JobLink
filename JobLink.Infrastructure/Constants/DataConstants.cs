namespace JobLink.Infrastructure.Constants
{
    public static class DataConstants
    {
        //Common
        public const int NameLength = 50;

        public const int PhoneMaxLength = 15;

        public const int PhoneMinLength = 7;

        public const int LocationMaxLength = 150;

        public const int LocationMinLength = 5;

        public const int UrlMaxLength = 500;

        //Job
        public const int JobTitleMaxLength = 50;

        public const int JobTitleMinLength = 10;

        public const int JobDescriptionMaxLength = 1000;

        public const int JobDescriptionMinLength = 50;

        public const string SalaryMinimum = "0";

        public const string SalaryMaximum = "1000000";

        //Employer
        public const int EmployerCompanyNameMaxLength = 30;

        public const int EmployerCompanyNameMinLength = 2;

        public const int EmployerWebsiteMaxLength = 50;

        public const int EmployerWebsiteMinLength = 5;


    }
}
