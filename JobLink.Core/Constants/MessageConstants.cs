namespace JobLink.Core.Constants
{
    public class MessageConstants
    {
        public const string RequiredMessage = "The {0} field is required";

        public const string LengthMessage = "The field {0} must be between {2} and {1} characters long";

        public const string PhoneExists = "Phone number already exists. Enter another one";

        public const string HasApplications = "You should have no job applications to become an employer";

        public const string CategoryDoNotExist = "Category does not exist";

        public const string NotAnApplicant = "The user is not an applicant";

        public const string AlreadyApplied = "The applicant is already applied for this job";

        public const string PositiveSalary = "Monthly Salary must be a positive number and less than {2} leva";

        public const string UserIsEmployer = "You should not be employer to become an applicant";

        public const string WrongCompanyOrId = "Wrong Company ID";
    }
}
