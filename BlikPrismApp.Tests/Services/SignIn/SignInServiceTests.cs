using BlikPrismApp.Services.SignIn;
using Moq;
using Shouldly;
using System.Net;
using System.Net.Http;
using Xunit;

namespace BlikPrismApp.UnitTests.Services.SignIn
{
    public class SignInServiceTests
    {
        [Theory]
        [InlineData("test_user", "test_password12")]
        [InlineData("@!%@#$^$^!#%^&987*$^%#$%^#$%^", "!@#$!@#$QT%!@$#!@#$$(#%&")]
        [InlineData("12321sdsa", "243SDSA@@@")]
        public void Service__ShouldSignInWithValidCredentials(string username, string password)
        {
            // ARRANGE
            var mockResponse = new Mock<HttpResponseMessage>(HttpStatusCode.OK);
            var mockApiService = new Mock<ISignInApiService>();
            mockApiService.Setup(s => s.SignInAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(mockResponse.Object);

            var userDto = new UserDto(username, password);
            var service = new SignInService(mockApiService.Object);

            // ACT
            var response = Should.NotThrow(service.SignInAsync(userDto));

            // ASSERT
            mockApiService.Verify(s => s.SignInAsync(It.IsAny<UserDto>()), Times.Once);

            response.ShouldBeTrue();
        }

        [Theory]
        [InlineData("adfasdf42342", null)]
        [InlineData("fasdf3#$@#$", null)]
        [InlineData("", null)]
        [InlineData(null, "OOIO(987*(*(")]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(null, null)]
        public void Service__ShouldFailSignInWithInvalidCredentials(string username, string password)
        {
            var shouldFail = !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);

            // ARRANGE
            var mockResponse = new Mock<HttpResponseMessage>(HttpStatusCode.BadRequest);
            var mockApiService = new Mock<ISignInApiService>();
            mockApiService.Setup(s => s.SignInAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(mockResponse.Object);

            var userDto = new UserDto(username, password);
            var service = new SignInService(mockApiService.Object);

            // ACT
            var response = Should.NotThrow(service.SignInAsync(userDto));

            // ASSERT
            mockApiService.Verify(s => s.SignInAsync(It.IsAny<UserDto>()), Times.Once);

            response.ShouldBe(shouldFail);
        }
    }
}
