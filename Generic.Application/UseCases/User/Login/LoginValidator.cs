using Generic.Comunication.DTO_s.Request.User;
using Generic.Comunication.Resources.User;
using FluentValidation;

namespace Generic.Application.UseCases.User.Login
{
    public class LoginValidator : AbstractValidator<RequestLoginJson>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage(UserResource.EMAIL_INVALID);
            RuleFor(x => x.Password)
                .SetValidator(new PasswordValidator<RequestLoginJson>());
        }
    }
}
