namespace JobLink.Core.Exceptions
{
    public class ApplicantAlreadyExistException : Exception
    {
        public ApplicantAlreadyExistException() { }

        public ApplicantAlreadyExistException(string message)
            : base(message) { }
    }
}
