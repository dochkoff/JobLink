using JobLink.Core.Exceptions;

namespace JobLink.Tests.UnitTests
{
    [TestFixture]
    public class ExceptionsTests
    {
        [Test]
        public void UnauthorizedActionConstructor_WithNoArguments_ShouldCreateExceptionWithNoMessage()
        {
            // Act
            var exception = new UnauthorizedActionException();

            // Assert
            Assert.IsNotNull(exception);
        }

        [Test]
        public void UnauthorizedActionConstructor_WithMessage_ShouldCreateExceptionWithMessage()
        {
            // Arrange
            var message = "Unauthorized action";

            // Act
            var exception = new UnauthorizedActionException(message);

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(message, exception.Message);
        }

        [Test]
        public void ApplicantAlreadyExistExceptionConstructor_WithNoArguments_ShouldCreateExceptionWithNoMessage()
        {
            // Act
            var exception = new ApplicantAlreadyExistException();

            // Assert
            Assert.IsNotNull(exception);
        }

        [Test]
        public void ApplicantAlreadyExistExceptionConstructor_WithMessage_ShouldCreateExceptionWithMessage()
        {
            // Arrange
            var message = "Applicant Already Exist";

            // Act
            var exception = new ApplicantAlreadyExistException(message);

            // Assert
            Assert.IsNotNull(exception);
            Assert.AreEqual(message, exception.Message);
        }
    }

}
