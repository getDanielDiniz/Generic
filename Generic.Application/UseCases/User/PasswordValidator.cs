using Generic.Comunication.Resources.User;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Generic.Application.UseCases.User
{
    public partial class PasswordValidator<T> : PropertyValidator<T, string>
    {
        public override string Name {get; } = "PasswordValidator";

        public override bool IsValid(ValidationContext<T> context, string password)
        {         
            if (string.IsNullOrWhiteSpace(password))
            {
                context.AddFailure(PasswordResource.PASSWORD_REQUIRED);
            }
            else
            {
                if (password.Length < 8)
                {
                    context.AddFailure(PasswordResource.PASSWORD_MIN_LENGTH);                    
                }
                if (!UpperCaseLetter().IsMatch(password))
                {
                    context.AddFailure(PasswordResource.PASSWORD_UPPERCASE_LETTER);
                }
                if (!Number().IsMatch(password))
                {
                    context.AddFailure(PasswordResource.PASSWORD_NUMBER);
                }
                if (!LowerCaseLetter().IsMatch(password))
                {
                    context.AddFailure(PasswordResource.PASSWORD_LOWERCASE_LETTER);
                }
                if (!SpecialCharacter().IsMatch(password))
                {
                    context.AddFailure(PasswordResource.PASSWORD_SPECIAL_CHARACTER);
                }
            }            
            return true;
        }

        [GeneratedRegex(@"[A-Z]+")]
        private static partial Regex UpperCaseLetter();
        
        [GeneratedRegex(@"[0-9]+")]
        private static partial Regex Number();

        [GeneratedRegex(@"[a-z]+")]
        private static partial Regex LowerCaseLetter();

        [GeneratedRegex(@"[\!\@\#\$\%]+")]
        private static partial Regex SpecialCharacter();        
    }
}
