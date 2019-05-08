using BlikPrismApp.Services.Blik;
using BlikPrismApp.Services.SignIn;
using Moq;
using Shouldly;
using System;
using Xunit;

namespace BlikPrismApp.UnitTests.Services.Blik
{
    public class BlikServiceTests
    {
        [Theory]
        [InlineData("123124124319847315369203150293561")]
        [InlineData("TEST_USER")]
        [InlineData("!$#RQFSDF#*$F(DA*A(*D(F*$(#*FASDFASDF#@(*")]
        public void Service__ShouldAskForBlikCodeWithCredentials(string username)
        {
            // ARRANGE
            var lowerBound = 100000;
            var upperBound = 1000000;
            var expectedBlikCode = new Random().Next(lowerBound, upperBound);

            var userDto = new UserDto(username, string.Empty);

            var mockApiService = new Mock<IBlikApiService>();
            mockApiService.Setup(s => s.GetBlikCode(userDto))
                .ReturnsAsync(expectedBlikCode);

            mockApiService.Setup(s => s.GetBlikCode(null))
                .Throws(new ArgumentNullException());

            var service = new BlikService(mockApiService.Object);

            // ACT
            var actualBlikCode = Should.NotThrow(service.GetBlikCodeAsync(userDto));

            // ASSERT
            mockApiService.Verify(s => s.GetBlikCode(It.IsAny<UserDto>()), Times.Once);

            actualBlikCode.ShouldBeInRange(lowerBound, upperBound);
            expectedBlikCode.ShouldBe(actualBlikCode);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void Service__ShouldFailWithInvalidCredentials(string username)
        {
            // ARRANGE
            var userDto = new UserDto(username, string.Empty);

            var mockApiService = new Mock<IBlikApiService>();
            mockApiService.Setup(s => s.GetBlikCode(userDto))
                .Throws<ArgumentException>();

            var service = new BlikService(mockApiService.Object);

            // ACT
            await Should.ThrowAsync<ArgumentException>(service.GetBlikCodeAsync(userDto));

            // ASSERT
            mockApiService.Verify(s => s.GetBlikCode(It.IsAny<UserDto>()), Times.Once);
        }
    }
}
