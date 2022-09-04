using CRMSample.Application.Common.Exceptions;
using CRMSample.Application.Identity.Account.Commands;
using CRMSample.Application.Identity.Services;
using CRMSample.Application.IdentityTests.Infrastructure;
using CRMSample.Domain.Identity.Entities.Account;
using CRMSample.Domain.Identity.ViewModels.Account;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.IdentityTests.Account.Commands
{
    public class LoginCommandHandlerTests : TestFixture
    {
        [Fact]
        public async Task GivenValidRequest_WhenUserDoesNotExist_ThenApiExceptionIsThrown()
        {
            // arrange
            LoginCommand command = new LoginCommand(Guid.NewGuid().ToString(), "password");

            LoginCommandHandler sut = new LoginCommandHandler(LoginService, TokenService, Mapper, NullLogger<LoginCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task GivenValidRequest_WhenUserExistsAndPasswordIsInvalid_ThenApiExceptionIsThrown()
        {
            // arrange
            ApplicationUser fakeUser = new ApplicationUser();

            MockLoginService
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeUser));

            LoginCommand command = new LoginCommand(Guid.NewGuid().ToString(), "password");

            LoginCommandHandler sut = new LoginCommandHandler(LoginService, TokenService, Mapper, NullLogger<LoginCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task GivenValidRequest_WhenUserExistsAndPasswordIsCorrect_ThenUserViewModelIsReturned()
        {
            // arrange
            ApplicationUser fakeUser = new ApplicationUser
            {
                Email = TestConstants.TestUserEmailAddress,
                UserName = TestConstants.TestUserName
            };

            MockLoginService
                .Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(fakeUser));

            MockLoginService
                .Setup(x => x.ValidateCredentialsAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            CreateTokenResult fakeToken = new CreateTokenResult(TestConstants.AccessToken);

            MockTokenService
                .Setup(x => x.CreateTokenAsync(It.IsAny<ApplicationUser>()))
                .Returns(Task.FromResult(fakeToken));

            LoginCommand command = new LoginCommand(Guid.NewGuid().ToString(), "password");

            LoginCommandHandler sut = new LoginCommandHandler(LoginService, TokenService, Mapper, NullLogger<LoginCommandHandler>.Instance);

            // act
            var response = await sut.Handle(command, CancellationToken.None);

            // assert
            response.Should().NotBeNull();
            response.Should().BeOfType<UserViewModel>();
            response.User.Should().NotBeNull();
            response.User.AccessToken.Should().Be(TestConstants.AccessToken);
            response.User.EmailAddress.Should().Be(TestConstants.TestUserEmailAddress);
            response.User.UserName.Should().Be(TestConstants.TestUserName);
        }
    }
}
