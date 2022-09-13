using FluentValidation;

namespace CRMSample.Application.Identity.Account.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Password)
                .NotNull()
                .MaximumLength(256);

            RuleFor(x => x.EmailAddress)
                .EmailAddress()
                .NotNull()
                .MaximumLength(256);
        }
    }
}
