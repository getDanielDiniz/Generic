using FluentValidation;
using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.Resources.User;

namespace Generic.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidator() { 
            RuleFor(x => x.newPassword)
                .NotEqual(x => x.oldPassword)
                .WithMessage(PasswordResource.PASSWORDS_EQUALS);
            RuleFor( x => x.newPassword)
                .SetValidator(new PasswordValidator<RequestChangePasswordJson>())
                .When(
                    x => !x.newPassword.Equals(x.oldPassword),
                    ApplyConditionTo.CurrentValidator
                );
        }
    }
}
