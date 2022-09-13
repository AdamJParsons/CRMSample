using FluentValidation;

namespace CRMSample.Application.Admin.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .MaximumLength(256);

            RuleFor(x => x.EmailAddress)
                .EmailAddress()
                .NotNull()
                .MaximumLength(256);

            RuleFor(x => x.ConfirmEmailAddress)
                .EmailAddress()
                .NotNull()
                .MaximumLength(256);

            RuleFor(x => x.JobTitle)
              .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .MaximumLength(100);
        }
    }
}
