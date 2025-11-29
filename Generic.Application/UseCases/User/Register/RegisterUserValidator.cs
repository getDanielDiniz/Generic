using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.Resources.User;
using FluentValidation;

namespace Generic.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage(UserResource.USERNAME_REQUIRED)
                .Length(3, 20).WithMessage(UserResource.USERNAME_INVALID_LENGTH)
                .When(x => !string.IsNullOrEmpty(x.Username), ApplyConditionTo.CurrentValidator);
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(UserResource.EMAIL_INVALID);
            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        }
    }
}
