using Generic.Application.UseCases.User.Register;
using Generic.Comunication.Resources.User;
using CommonTestsUtilities.Builders.Requests.User;
using FluentAssertions;

namespace UnitTests.Validators.User
{
    public class RegisterUserValidatorUnitTest
    {
        [Fact]
        public void RegisterUserValidator_ValidData()
        {
            // Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData("")]
        [InlineData("     ")]
        [InlineData("email")]
        [InlineData("email@")]
        [InlineData("@email")]
        public void RegisterUserValidator_InvalidEmail(string email)
        {
            // Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            request.Email = email;

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == UserResource.EMAIL_INVALID);
        }

        [Fact]
        public void RegisterUserValidator_EmptyPassword()
        {
            // Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            request.Password = "";

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == PasswordResource.PASSWORD_REQUIRED);
        }

        [Fact]
        public void RegisterUserValidator_EmptyUsername()
        {
            // Arrange
            var validator = new RegisterUserValidator();
            var request = RequestRegisterUserBuilder.Build();
            request.Username = "";

            // Act
            var result = validator.Validate(request);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.ErrorMessage == UserResource.USERNAME_REQUIRED);
        }
    }
}
