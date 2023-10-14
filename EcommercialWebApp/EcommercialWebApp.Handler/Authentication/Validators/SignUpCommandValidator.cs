using EcommercialWebApp.Handler.Authentication.Commands;
using FluentValidation;

namespace EcommercialWebApp.Handler.Authentication.Validators
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(6);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.PasswordConfirmation).NotEmpty().Equal(x => x.Password);
        }
    }
}
