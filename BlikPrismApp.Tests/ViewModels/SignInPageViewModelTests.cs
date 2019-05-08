using BlikPrismApp.Services.SignIn;
using BlikPrismApp.ViewModels;
using Moq;
using Prism.Navigation;
using Prism.Services;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace BlikPrismApp.UnitTests.ViewModels
{
    public class SignInPageViewModelTests
    {
        [Theory]
        [InlineData("test_user1", "test_password12")]
        [InlineData("ąążśźęćń€€óóę", "śżćó€źźó")]
        [InlineData("1341234123515asdfasdf", "fasdfa32534")]
        public void ViewModel__ShouldHandleSignInSuccess(string username, string password)
        {
            // ARRANGE
            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>()))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var mockDialogService = new Mock<IPageDialogService>();
            mockDialogService.Setup(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var mockSignInService = new Mock<ISignInService>();
            mockSignInService.Setup(s => s.SignInAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(true);

            var vm = new SignInPageViewModel(mockNavigationService.Object, mockDialogService.Object, mockSignInService.Object)
            {
                Username = username,
                Password = password
            };

            // ACT
            Should.NotThrow(vm.LoginCommand.Execute);

            // ASSERT
            mockNavigationService.Verify(n => n.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>()), Times.Once);
            mockSignInService.Verify(s => s.SignInAsync(It.IsAny<UserDto>()), Times.Once);
            mockDialogService.Verify(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData("test_user1", "test_password12")]
        [InlineData("ąążśźęćń€€óóę", "śżćó€źźó")]
        [InlineData("1341234123515asdfasdf", "fasdfa32534")]
        public void LoginCommand__ShouldHandleSignInFailure(string username, string password)
        {
            // ARRANGE
            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>()))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var mockDialogService = new Mock<IPageDialogService>();
            mockDialogService.Setup(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var mockSignInService = new Mock<ISignInService>();
            mockSignInService.Setup(s => s.SignInAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(false);

            var vm = new SignInPageViewModel(mockNavigationService.Object, mockDialogService.Object, mockSignInService.Object)
            {
                Username = username,
                Password = password
            };

            // ACT
            Should.NotThrow(vm.LoginCommand.Execute);

            // ASSERT
            mockNavigationService.Verify(n => n.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>()), Times.Never);
            mockSignInService.Verify(s => s.SignInAsync(It.IsAny<UserDto>()), Times.Once);
            mockDialogService.Verify(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData(null, null)]
        public void LoginCommand__ShouldNotSignInOnInvalidCredentials(string username, string password)
        {
            // ARRANGE
            var mockNavigationService = new Mock<INavigationService>();
            mockNavigationService.Setup(n => n.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>()))
                .ReturnsAsync(new Mock<INavigationResult>().Object);

            var mockDialogService = new Mock<IPageDialogService>();
            mockDialogService.Setup(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            var mockSignInService = new Mock<ISignInService>();
            mockSignInService.Setup(s => s.SignInAsync(It.IsAny<UserDto>()))
                .ReturnsAsync(false);

            var vm = new SignInPageViewModel(mockNavigationService.Object, mockDialogService.Object, mockSignInService.Object)
            {
                Username = username,
                Password = password
            };

            // ACT
            Should.NotThrow(vm.LoginCommand.Execute);

            // ASSERT
            mockNavigationService.Verify(n => n.NavigateAsync(It.IsAny<string>(), It.IsAny<INavigationParameters>()), Times.Never);
            mockSignInService.Verify(s => s.SignInAsync(It.IsAny<UserDto>()), Times.Never);
            mockDialogService.Verify(d => d.DisplayAlertAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
