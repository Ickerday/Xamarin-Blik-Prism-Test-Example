using BlikPrismApp.Services.Blik;
using BlikPrismApp.Services.SignIn;
using BlikPrismApp.ViewModels;
using BlikPrismApp.Views;
using Moq;
using Prism.Navigation;
using Prism.Services;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BlikPrismApp.UnitTests.ViewModels
{
    public class BlikCodePageViewModelTests
    {
        [Theory]
        [InlineData("test_user1")]
        [InlineData("ąążśźęćń€€óóę")]
        [InlineData("1341234123515asdfasdf")]
        public void OnNavigatingTo__ShouldParseNavigationParametersCorrectly(string username)
        {
            // ARRANGE
            var expectedBlikCode = 100200;
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IPageDialogService>();
            var mockBlikService = new Mock<IBlikService>();
            mockBlikService.Setup(s => s.GetBlikCodeAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(expectedBlikCode);

            var viewModel = new BlikCodePageViewModel(mockNavigationService.Object, mockBlikService.Object, mockDialogService.Object);

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatingTo(parameters));

            // ASSERT
            viewModel.BlikCode.ShouldBe(expectedBlikCode);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void OnNavigatingTo__ShouldNavigateBackOnInvalidCredentials(string username)
        {
            // ARRANGE
            var expectedBlikCode = 100200;
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IPageDialogService>();
            var mockBlikService = new Mock<IBlikService>();
            mockBlikService.Setup(s => s.GetBlikCodeAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(expectedBlikCode);

            var viewModel = new BlikCodePageViewModel(mockNavigationService.Object, mockBlikService.Object, mockDialogService.Object);

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatingTo(parameters));

            // ASSERT
            mockNavigationService.Verify(n => n.GoBackAsync(), Times.Once);
        }

        [Theory]
        [InlineData("test_user1")]
        [InlineData("ąążśźęćń€€óóę")]
        [InlineData("1341234123515asdfasdf")]
        public void OnNavigatingTo__DisplaysErrorAlertOnSignInException(string username)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IPageDialogService>();
            mockDialogService.Setup(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var mockBlikService = new Mock<IBlikService>();
            mockBlikService.Setup(s => s.GetBlikCodeAsync(It.IsAny<UserDto>()))
                .Throws<ArgumentException>();

            var viewModel = new BlikCodePageViewModel(mockNavigationService.Object, mockBlikService.Object, mockDialogService.Object);

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatingTo(parameters));

            // ASSERT
            mockDialogService.Verify(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void OnNavigatedTo__NavigatesBackOnInvalidBlikCode()
        {
            // ARRANGE
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IPageDialogService>();
            var mockBlikService = new Mock<IBlikService>();

            var viewModel = new BlikCodePageViewModel(mockNavigationService.Object, mockBlikService.Object, mockDialogService.Object)
            { BlikCode = null };

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatedTo(new Mock<INavigationParameters>().Object));

            // ASSERT
            mockNavigationService.Verify(n => n.GoBackAsync(), Times.Once);
        }

        [Theory(Skip = "Unable to test Device.StartTimer()")]
        [InlineData("test_user1")]
        [InlineData("ąążśźęćń€€óóę")]
        [InlineData("1341234123515asdfasdf")]
        public void OnNavigatedTo__NavigatesToBlikConfirmationPageOnValidCredentials(string username)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>()))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var mockDialogService = new Mock<IPageDialogService>();
            mockDialogService.Setup(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var mockBlikService = new Mock<IBlikService>();

            var viewModel = new BlikCodePageViewModel(mockNavigationService.Object, mockBlikService.Object, mockDialogService.Object)
            { BlikCode = 100200 };

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatedTo(parameters));

            // ASSERT
            mockNavigationService.Verify(n => n.NavigateAsync($"{nameof(BlikConfirmationPage)}",
                It.IsAny<INavigationParameters>()), Times.Once);

        }
    }
}
