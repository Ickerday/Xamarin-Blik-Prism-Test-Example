using BlikPrismApp.ViewModels;
using BlikPrismApp.Views;
using Moq;
using Prism.Navigation;
using Shouldly;
using Xunit;

namespace BlikPrismApp.UnitTests.ViewModels
{
    public class AccountPageViewModelTests
    {
        [Theory]
        [InlineData("test_user1")]
        [InlineData("ąążśźęćń€€óóę")]
        [InlineData("1341234123515asdfasdf")]
        public void GetBlikCodeCommand__ShouldNavigateToCorrectView(string username)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync($"{nameof(BlikCodePage)}",
                new NavigationParameters($"{nameof(username)}={username}")))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var viewModel = new AccountPageViewModel(mockNavigationService.Object);

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatingTo(parameters));
            Should.NotThrow(viewModel.GetBlikCodeCommand.Execute);

            // ASSERT
            mockNavigationService.Verify(n => n.NavigateAsync($"{nameof(BlikCodePage)}",
                new NavigationParameters($"{nameof(username)}={username}")), Times.Once);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void GetBlikCodeCommand__ShouldNotNavigateOnInvalidUsername(string username)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync($"{nameof(BlikCodePage)}",
                new NavigationParameters($"{nameof(username)}={username}")))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var viewModel = new AccountPageViewModel(mockNavigationService.Object);

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatingTo(parameters));

            // ASSERT
            viewModel.GetBlikCodeCommand.CanExecute().ShouldBeFalse();
        }

        [Theory]
        [InlineData("test_user1")]
        [InlineData("ąążśźęćń€€óóę")]
        [InlineData("1341234123515asdfasdf")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void OnNavigatingTo__ParserNavigationParametersCorrectly(string username)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync($"/{nameof(SignInPage)}",
                new NavigationParameters($"{nameof(username)}={username}")))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var viewModel = new AccountPageViewModel(mockNavigationService.Object);

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatingTo(parameters));

            // ASSERT
            viewModel.Username.ShouldBeSameAs(username ?? string.Empty);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void OnNavigatingTo__NavigatesToSignInPageOnInvalidUsername(string username)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            { { nameof(username), username }, };

            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync($"/{nameof(SignInPage)}",
                new NavigationParameters($"{nameof(username)}={username}")))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var viewModel = new AccountPageViewModel(mockNavigationService.Object);

            // ACT
            Should.NotThrow(() => viewModel.OnNavigatingTo(parameters));

            // ASSERT
            mockNavigationService.Verify(n => n.NavigateAsync($"/{nameof(SignInPage)}",
                new NavigationParameters($"{nameof(username)}={username}")), Times.Never);

        }
    }
}
