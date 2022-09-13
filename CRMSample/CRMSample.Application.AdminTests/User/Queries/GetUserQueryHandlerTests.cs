using CRMSample.Application.Admin.User.Queries;
using CRMSample.Application.Common.Exceptions;
using CRMSample.Domain.Admin.Entities.User;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSample.Application.AdminTests.User.Queries
{
    public class GetUserQueryHandlerTests : TestFixture
    {
        [Fact]
        public async Task GivenValidRequest_WhenUserDoesNotExist_ThenApiExceptionIsThrown()
        {
            // arrange
            GetUserQuery query = new GetUserQuery(1);

            GetUserQueryHandler sut = new GetUserQueryHandler(UserService, Mapper, NullLogger<GetUserQueryHandler>.Instance);

            // act / assert
            await Assert.ThrowsAsync<CrmApiException>(() => sut.Handle(query, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task GivenValidRequest_WhenUserExists_ThenUserViewModelIsReturned()
        {
            // arrange
            ApplicationUserModel fakeUser = new ApplicationUserModel
            {
                Id = TestConstants.TestUserId,
                UserName = TestConstants.TestUserName,
                EmailAddress = TestConstants.TestUserEmailAddress,
                JobTitle = TestConstants.TestUserJobTitle,
                IntegrationId = new Guid(TestConstants.TestUserIntegrationId)
            };
            MockUserService
                .Setup(x => x.GetUserByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(fakeUser));

            GetUserQuery query = new GetUserQuery(1);

            GetUserQueryHandler sut = new GetUserQueryHandler(UserService, Mapper, NullLogger<GetUserQueryHandler>.Instance);

            // act
            var response = await sut.Handle(query, It.IsAny<CancellationToken>());

            // assert
            response.Should().NotBeNull();
            response.User.Should().NotBeNull();
            response.User.Id.Should().Be(TestConstants.TestUserId);
            response.User.UserName.Should().Be(TestConstants.TestUserName);
            response.User.EmailAddress.Should().Be(TestConstants.TestUserEmailAddress);
            response.User.JobTitle.Should().Be(TestConstants.TestUserJobTitle);
        }
    }
}
