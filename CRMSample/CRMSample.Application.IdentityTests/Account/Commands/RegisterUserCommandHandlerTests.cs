using CRMSample.Application.Common.Exceptions;
using CRMSample.Application.Identity.Account.Commands.RegisterUser;
using CRMSample.Application.Identity.Services;
using CRMSample.Domain.Identity.Entities.Account;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.IdentityTests.Account.Commands
{
    public class RegisterUserCommandHandlerTests : TestFixture
    {
        [Fact]
        public async Task GivenValidRequest_WhenTheUserAlreadyExists_ThrowsApiException()
        {
            // arrange
            ApplicationUser fakeUser = new ApplicationUser();

            MockLoginService
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeUser));

            RegisterUserCommand command = new RegisterUserCommand(
                new Guid(TestConstants.TestUserIntegrationId),
                TestConstants.TestUserName,
                TestConstants.TestUserEmailAddress,
                TestConstants.TestUserEmailAddress,
                TestConstants.TestUserPassword,
                TestConstants.TestUserPassword);

            RegisterUserCommandHandler sut = new RegisterUserCommandHandler(LoginService, Mapper, Mediator, NullLogger<RegisterUserCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(TestConstants.TestUserPassword, "INVALID")]
        [InlineData("INVALID", TestConstants.TestUserPassword)]
        public async Task GivenValidRequest_WhenPasswordAndConfirmPasswordDoNotMatch_ThrowsApiException(string password, string confirmPassword)
        {
            // arrange
            RegisterUserCommand command = new RegisterUserCommand(
                new Guid(TestConstants.TestUserIntegrationId),
                TestConstants.TestUserName,
                TestConstants.TestUserEmailAddress,
                TestConstants.TestUserEmailAddress,
                password,
                confirmPassword);

            RegisterUserCommandHandler sut = new RegisterUserCommandHandler(LoginService, Mapper, Mediator, NullLogger<RegisterUserCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Theory]
        [InlineData(TestConstants.TestUserEmailAddress, "INVALID@INVALID.COM")]
        [InlineData("INVALID@INVALID.COM", TestConstants.TestUserEmailAddress)]
        public async Task GivenValidRequest_WhenEmailAndConfirmEmailDoNotMatch_ThrowsApiException(string email, string confirmEmail)
        {
            // arrange
            RegisterUserCommand command = new RegisterUserCommand(
                new Guid(TestConstants.TestUserIntegrationId),
                TestConstants.TestUserName,
                email,
                confirmEmail,
                TestConstants.TestUserPassword,
                TestConstants.TestUserPassword);

            RegisterUserCommandHandler sut = new RegisterUserCommandHandler(LoginService, Mapper, Mediator, NullLogger<RegisterUserCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GivenValidRequest_WhenSuccessful_ReturnsUserViewModel()
        {
            // arrange
            ApplicationUser fakeUser = new ApplicationUser
            {
                UserName = TestConstants.TestUserName,
                Email = TestConstants.TestUserEmailAddress,
                IntegrationId = new Guid(TestConstants.TestUserIntegrationId)
            };

            MockLoginService
                .Setup(x => x.CreateUserAsync(It.IsAny<CreateUserRequest>()))
                .Returns(Task.FromResult(fakeUser));

            RegisterUserCommand command = new RegisterUserCommand(
                new Guid(TestConstants.TestUserIntegrationId),
                TestConstants.TestUserName,
                TestConstants.TestUserEmailAddress,
                TestConstants.TestUserEmailAddress,
                TestConstants.TestUserPassword,
                TestConstants.TestUserPassword);

            RegisterUserCommandHandler sut = new RegisterUserCommandHandler(LoginService, Mapper, Mediator, NullLogger<RegisterUserCommandHandler>.Instance);

            // act
            var response = await sut.Handle(command, It.IsAny<CancellationToken>());

            // assert
            response.Should().NotBeNull();
            response.User.Should().NotBeNull();
            response.User.IntegrationId.Should().Be(TestConstants.TestUserIntegrationId);
            response.User.UserName.Should().Be(command.UserName);
            response.User.EmailAddress.Should().Be(command.Email);
        }
    }
}
