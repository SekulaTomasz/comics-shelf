using Autofac.Extras.Moq;
using comics_shelf_api.core.Repositories;
using comics_shelf_api.core.Repositories.Interfaces;
using comics_shelf_api.core.Services;
using comics_shelf_api.core.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace comics_shelf_api.test
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public async Task Should_Return_Response_With_StatusCode_200()
        {
            var userId = Guid.NewGuid();
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock

                mock.Mock<IUserRepository>().Setup(x => x.FindUserByIdAsync(userId)).ReturnsAsync(new core.Models.User() { 
                    Id = userId
                });
                var sut = mock.Create<UserService>();

                // Act
                var actual = await sut.FindUserByIdAsync(userId);

                // Assert - assert on the mock
                Assert.AreEqual(HttpStatusCode.OK, actual.StatusCode);
            }
        }
        [TestMethod]
        public async Task Should_Return_Response_With_StatusCode_400_And_Empty_Result()
        {
            var userId = Guid.NewGuid();
            var userId2 = Guid.NewGuid();
            using (var mock = AutoMock.GetLoose())
            {
                // Arrange - configure the mock

                mock.Mock<IUserRepository>().Setup(x => x.FindUserByIdAsync(userId)).ReturnsAsync(new core.Models.User()
                {
                    Id = userId
                });
                var sut = mock.Create<UserService>();

                // Act
                var actual = await sut.FindUserByIdAsync(userId2);

                // Assert - assert on the mock
                Assert.AreEqual(HttpStatusCode.BadRequest, actual.StatusCode);
                Assert.IsNull(actual.Result);
            }
        }
    }
}
