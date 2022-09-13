using CRMSample.Application.Admin.User.Commands.UpdateUser;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Domain.Admin.Dtos.User;
using CRMSample.Domain.Admin.Entities.User;
using Microsoft.Extensions.Logging.Abstractions;

namespace CRMSample.Application.AdminTests.User.Commands
{
    public class UpdateUserCommandHandlerTests : TestFixture
    {
        [Fact]
        public async Task GivenValidRequest_WhenUserDoesNotExist_ThenApiExceptionIsThrown()
        {
            // arrange
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = 99
            };
            UpdateUserCommand command = new UpdateUserCommand(updateUserDto);

            UpdateUserCommandHandler sut = new UpdateUserCommandHandler(
                UserService, 
                Mapper, 
                MachineDateTime,
                NullLogger<UpdateUserCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GivenValidRequest_WhenUserExistsAndEmailIsChangedToOneAlreadyInUse_ThenApiExceptionIsThrown()
        {
            // arrange

            // the user we are updating
            ApplicationUserModel existingUser = new ApplicationUserModel
            {
                Id = 1,
                EmailAddress = TestConstants.TestUserEmailAddress,
                UserName = TestConstants.TestUserName,
                JobTitle = TestConstants.TestUserJobTitle
            };

            // the user we know has a matching email address
            ApplicationUserModel existingSecondUser = new ApplicationUserModel
            {
                Id = 2,
                EmailAddress = "47A531F7-2837-4128-A29C-D3F6A7042D62@nowhere.com",
                UserName = TestConstants.TestUserName,
                JobTitle = TestConstants.TestUserJobTitle
            };

            MockUserService
                .Setup(x => x.GetUserByIdAsync(It.Is<long>(id => id == 1), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(existingUser));

            MockUserService
                .Setup(x => x.GetUserByEmailAsync(It.Is<string>(email => email == existingSecondUser.EmailAddress), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(existingSecondUser));

            // attempt to update existingUser with the same email address as existingSecondUser
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = 1,
                UserName = TestConstants.TestUserName,
                EmailAddress = existingSecondUser.EmailAddress,
                JobTitle = TestConstants.TestUserJobTitle
            };
            UpdateUserCommand command = new UpdateUserCommand(updateUserDto);

            UpdateUserCommandHandler sut = new UpdateUserCommandHandler(
                UserService,
                Mapper,
                MachineDateTime,
                NullLogger<UpdateUserCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GivenValidRequest_WhenUserExistsAndUsernameIsChangedToOneAlreadyInUse_ThenApiExceptionIsThrown()
        {
            // arrange

            // the user we are updating
            ApplicationUserModel existingUser = new ApplicationUserModel
            {
                Id = 1,
                EmailAddress = TestConstants.TestUserEmailAddress,
                UserName = TestConstants.TestUserName,
                JobTitle = TestConstants.TestUserJobTitle
            };

            // the user we know has a matching email address
            ApplicationUserModel existingSecondUser = new ApplicationUserModel
            {
                Id = 2,
                EmailAddress = TestConstants.TestUserEmailAddress,
                UserName = "971385BF-4FF5-4B4C-8422-27497218E537",
                JobTitle = TestConstants.TestUserJobTitle
            };

            MockUserService
                .Setup(x => x.GetUserByIdAsync(It.Is<long>(id => id == 1), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(existingUser));

            MockUserService
                .Setup(x => x.GetUserByUserNameAsync(It.Is<string>(username => username == existingSecondUser.UserName), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(existingSecondUser));

            // attempt to update existingUser with the same email address as existingSecondUser
            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = 1,
                UserName = existingSecondUser.UserName,
                EmailAddress = TestConstants.TestUserEmailAddress,
                JobTitle = TestConstants.TestUserJobTitle
            };
            UpdateUserCommand command = new UpdateUserCommand(updateUserDto);

            UpdateUserCommandHandler sut = new UpdateUserCommandHandler(
                UserService,
                Mapper,
                MachineDateTime,
                NullLogger<UpdateUserCommandHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GivenValidRequest_WhenUserExists_ThenUserIsUpdatedAndViewModelIsReturned()
        {
            // arrange
            ApplicationUserModel existingUser = new ApplicationUserModel
            {
                EmailAddress = TestConstants.TestUserEmailAddress,
                UserName = TestConstants.TestUserName,
                JobTitle = TestConstants.TestUserJobTitle
            };

            MockUserService
                .Setup(x => x.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(existingUser));

            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = 1,
                UserName = "D0EE0BEB-D767-470E-A3EF-5B4EF4B1AC4A",
                EmailAddress = "47A531F7-2837-4128-A29C-D3F6A7042D62@nowhere.com"
            };
            UpdateUserCommand command = new UpdateUserCommand(updateUserDto);

            UpdateUserCommandHandler sut = new UpdateUserCommandHandler(
                UserService,
                Mapper,
                MachineDateTime,
                NullLogger<UpdateUserCommandHandler>.Instance);

            // act
            var response = await sut.Handle(command, It.IsAny<CancellationToken>());

            // assert
            response.Should().NotBeNull();
            response.User.Should().NotBeNull();
            response.User.UserName.Should().Be(updateUserDto.UserName);
            response.User.EmailAddress.Should().Be(updateUserDto.EmailAddress);
            response.User.JobTitle.Should().Be(updateUserDto.JobTitle);
            response.User.DateModified.Should().Be(MachineDateTime.Now);
        }

        [Fact]
        public async Task GivenValidRequest_WhenUserExists_ThenPersonIsUpdatedAndViewModelIsReturned()
        {
            // arrange
            ApplicationUserModel existingUser = new ApplicationUserModel
            {
                EmailAddress = TestConstants.TestUserEmailAddress,
                UserName = TestConstants.TestUserName,
                JobTitle = TestConstants.TestUserJobTitle
            };

            MockUserService
                .Setup(x => x.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(existingUser));

            UpdateUserDto updateUserDto = new UpdateUserDto
            {
                Id = 1,
                EmailAddress = TestConstants.TestUserEmailAddress,
                UserName = TestConstants.TestUserName,
                JobTitle = TestConstants.TestUserJobTitle,
                PersonGenderId = TestConstants.TestUserPersonGenderId,
                PersonTitleId = TestConstants.TestUserPersonTitleId,
                PersonForename = TestConstants.TestUserPersonForename,
                PersonSurname = TestConstants.TestUserPersonSurname,
                PersonOtherNames = TestConstants.TestUserPersonOtherNames
            };
            UpdateUserCommand command = new UpdateUserCommand(updateUserDto);

            UpdateUserCommandHandler sut = new UpdateUserCommandHandler(
                UserService,
                Mapper,
                MachineDateTime,
                NullLogger<UpdateUserCommandHandler>.Instance);

            // act
            var response = await sut.Handle(command, It.IsAny<CancellationToken>());

            // assert
            response.Should().NotBeNull();
            response.User.Should().NotBeNull();
            response.User.PersonTitleId.Should().Be(updateUserDto.PersonTitleId);
            response.User.PersonGenderId.Should().Be(updateUserDto.PersonGenderId);
            response.User.PersonForename.Should().Be(updateUserDto.PersonForename);
            response.User.PersonOtherNames.Should().Be(updateUserDto.PersonOtherNames);
            response.User.PersonSurname.Should().Be(updateUserDto.PersonSurname);
        }
    }
}
