using CRMSample.Application.Admin.User.Commands.CreateUser;
using CRMSample.Application.Common.Events.CreateUser;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Domain.Admin.Entities.Person;
using CRMSample.Domain.Admin.Entities.User;
using MassTransit;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.AdminTests.User.Commands
{

    public class CreateUserCommandHandlerTests : TestFixture
    {
        //private Mock<IRequestClient<RegisterUserRequest>> _mockRequestClient;

        //public CreateUserCommandHandlerTests()
        //{
        //    _mockRequestClient = new Mock<IRequestClient<RegisterUserRequest>>();
        //}

        //[Fact]
        //public async Task GivenValidRequest_WhenUserNameExists_ThenApiExceptionIsThrown()
        //{
        //    // arrange
        //    ApplicationUserModel existingUser = CreateExistingUser();

        //    MockUserService
        //        .Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        //        .Returns(Task.FromResult(existingUser));

        //    CreateUserCommand command = CreateStandardCreateUserCommand();

        //    CreateUserCommandHandler sut = new CreateUserCommandHandler(
        //        UserService, 
        //        Mapper,
        //        MachineDateTime,
        //        _mockRequestClient.Object, 
        //        NullLogger<CreateUserCommandHandler>.Instance);

        //    // act / assert
        //    await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        //}

        //[Fact]
        //public async Task GivenValidRequest_WhenEmailExists_ThenApiExceptionIsThrown()
        //{
        //    // arrange
        //    ApplicationUserModel existingUser = CreateExistingUser();

        //    MockUserService
        //        .Setup(x => x.GetUserByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        //        .Returns(Task.FromResult(existingUser));

        //    CreateUserCommand command = CreateStandardCreateUserCommand();

        //    CreateUserCommandHandler sut = new CreateUserCommandHandler(
        //        UserService,
        //        Mapper,
        //        MachineDateTime,
        //        _mockRequestClient.Object,
        //        NullLogger<CreateUserCommandHandler>.Instance);

        //    // act / assert
        //    await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(command, It.IsAny<CancellationToken>()));
        //}

        //[Fact]
        //public async Task GivenValidRequest_WhenSuccessful_ThenUserViewModelIsReturned()
        //{
        //    // arrange
        //    ApplicationUserModel existingUser = CreateExistingUser();

        //    MockUserService
        //        .Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        //        .Returns(Task.FromResult((ApplicationUserModel)null));

        //    CreateUserCommand command = CreateStandardCreateUserCommand();

        //    CreateUserCommandHandler sut = new CreateUserCommandHandler(
        //        UserService,
        //        Mapper,
        //        MachineDateTime,
        //        _mockRequestClient.Object,
        //        NullLogger<CreateUserCommandHandler>.Instance);

        //    // act
        //    var response = await sut.Handle(command, CancellationToken.None);

        //    // assert
        //    response.Should().NotBeNull();
        //    response.User.Should().NotBeNull();
        //    response.User.IntegrationId.Should().NotBe(Guid.Empty);
        //    response.User.UserName.Should().Be(TestConstants.TestUserName);
        //    response.User.EmailAddress.Should().Be(TestConstants.TestUserEmailAddress);
        //    response.User.JobTitle.Should().Be(TestConstants.TestUserJobTitle);
        //}

        //[Fact]
        //public async Task GivenValidRequest_WhenSuccessful_ThenPersonIsCreateAndUserViewModelIsReturned()
        //{
        //    // arrange
        //    ApplicationUserModel existingUser = CreateExistingUser();

        //    MockUserService
        //        .Setup(x => x.GetUserByUserNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
        //        .Returns(Task.FromResult((ApplicationUserModel)null));

        //    CreateUserCommand command = CreateStandardCreateUserCommand();

        //    CreateUserCommandHandler sut = new CreateUserCommandHandler(
        //        UserService,
        //        Mapper,
        //        MachineDateTime,
        //        _mockRequestClient.Object,
        //        NullLogger<CreateUserCommandHandler>.Instance);

        //    // act
        //    var response = await sut.Handle(command, CancellationToken.None);

        //    // assert
        //    response.Should().NotBeNull();
        //    response.User.Should().NotBeNull();
        //    response.User.PersonGenderId.Should().Be(command.GenderId);
        //    response.User.PersonTitleId.Should().Be(command.TitleId);
        //    response.User.PersonForename.Should().Be(command.Forename);
        //    response.User.PersonOtherNames.Should().Be(command.OtherNames);
        //}

        //[Fact]
        //public async Task GivenValidRequest_WhenSuccessfulAndCreateUserIntegrationEventFails_ThenUserIsNotCreated()
        //{
        //    Assert.Fail("Not implemented");
        //}

        //private CreateUserCommand CreateStandardCreateUserCommand()
        //{
        //    CreateUserCommand command = new CreateUserCommand(
        //        TestConstants.TestUserName,
        //        TestConstants.TestUserPassword,
        //        TestConstants.TestUserPassword,
        //        TestConstants.TestUserEmailAddress,
        //        TestConstants.TestUserEmailAddress,
        //        TestConstants.TestUserPersonForename,
        //        TestConstants.TestUserPersonOtherNames,
        //        TestConstants.TestUserPersonSurname,
        //        TestConstants.TestUserPersonTitleId,
        //        TestConstants.TestUserPersonGenderId);

        //    command.JobTitle = TestConstants.TestUserJobTitle;
        //    return command;
        //}

        //private ApplicationUserModel CreateExistingUser()
        //{
        //    return new ApplicationUserModel
        //    {
        //        IntegrationId = new Guid(TestConstants.TestUserIntegrationId),
        //        UserName = TestConstants.TestUserName,
        //        EmailAddress = TestConstants.TestUserEmailAddress,
        //        JobTitle = TestConstants.TestUserJobTitle,
        //        Person = new PersonModel
        //        {
        //            TitleId = TestConstants.TestUserPersonTitleId,
        //            GenderId = TestConstants.TestUserPersonGenderId,
        //            Forename = TestConstants.TestUserPersonForename,
        //            OtherNames = TestConstants.TestUserPersonOtherNames,
        //            Surname = TestConstants.TestUserPersonSurname
        //        }
        //    };
        //}
    }
}
