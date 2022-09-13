using FluentValidation;

namespace CRMSample.Application.Identity.Account.Commands.RegisterUser
{
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.ConfirmEmail)
                .NotEmpty()
                .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
