using FluentValidation;

namespace CRMSample.Application.Admin.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.User).NotNull();

            RuleFor(x => x.User.UserName)
                .NotNull()
                .MaximumLength(256);

            RuleFor(x => x.User.EmailAddress)
                .EmailAddress()
                .NotNull()
                .MaximumLength(256);

            RuleFor(x => x.User.JobTitle)
              .MaximumLength(256);
        }
    }
}
