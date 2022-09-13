using CRMSample.Domain.Admin.ViewModels.User;
using MediatR;

namespace CRMSample.Application.Admin.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<UserViewModel>
    {
        public CreateUserCommand(
            string userName,
            string password,
            string confirmPassword,
            string emailAddress,
            string confirmEmailAddress,
            string forename,
            string othernames,
            string surname,
            long? titleId = null,
            long? genderId = null)
        {
            UserName = userName;
            Password = password;
            ConfirmPassword = confirmPassword;
            EmailAddress = emailAddress;
            ConfirmEmailAddress = confirmEmailAddress;
            Forename = forename;
            OtherNames = othernames;
            Surname = surname;
            TitleId = titleId;
            GenderId = genderId;
        }

        public string UserName { get; }
        public string Password { get; }
        public string ConfirmPassword { get; }
        public string EmailAddress { get; }
        public string ConfirmEmailAddress { get; }
        public string Forename { get; }
        public string OtherNames { get; }
        public string Surname { get; }
        public long? TitleId { get; }
        public long? GenderId { get; }
        public string? JobTitle { get; set; }
    }
}
