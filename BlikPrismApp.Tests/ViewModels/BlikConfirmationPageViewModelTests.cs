using BlikPrismApp.ViewModels;
using Moq;
using Prism.Navigation;
using Shouldly;
using Xunit;

namespace BlikPrismApp.UnitTests.ViewModels
{
    public class BlikConfirmationPageViewModelTests
    {
        [Theory]
        [InlineData("test_user1", "test_password12", 420)]
        [InlineData("ąążśźęćń€€óóę", "śżćó€źźó", 2137)]
        [InlineData("1341234123515asdfasdf", "fasdfa32534", 47)]
        public void OnNavigatingTo__ShouldParseNavigationParametersCorrectly(string recipient, string operationName, int amount)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            {
                { nameof(recipient), recipient },
                { nameof(operationName), operationName },
                { nameof(amount), amount }
            };
            var mockNavigationService = new Mock<INavigationService>();

            var vm = new BlikConfirmationPageViewModel(mockNavigationService.Object);

            // ACT
            Should.NotThrow(() => vm.OnNavigatingTo(parameters));

            // ASSERT
            vm.Recipient.ShouldBeSameAs(recipient);
            vm.OperationName.ShouldBeSameAs(operationName);
            vm.Amount.ShouldBe(amount);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData("", null, 420)]
        [InlineData(" ", null, 420)]
        [InlineData(null, "", null)]
        [InlineData(null, " ", null)]
        [InlineData("", "", 0)]
        [InlineData("", "", null)]
        [InlineData(" ", "", 0)]
        [InlineData(" ", "", null)]
        [InlineData("", " ", 0)]
        [InlineData("", " ", null)]
        [InlineData(" ", " ", 0)]
        [InlineData(" ", " ", null)]
        public void OnNavigatingTo__ShouldSetDefaultValuesWithWrongNavigationParameters(string recipient, string operationName, int? amount)
        {
            // ARRANGE
            var parameters = new NavigationParameters
            {
                { nameof(recipient), recipient },
                { nameof(operationName), operationName },
                { nameof(amount), amount }
            };
            var mockNavigationService = new Mock<INavigationService>();

            var vm = new BlikConfirmationPageViewModel(mockNavigationService.Object);

            // ACT
            Should.NotThrow(() => vm.OnNavigatingTo(parameters));

            // ASSERT
            vm.Recipient.ShouldBeSameAs(recipient);
            vm.OperationName.ShouldBeSameAs(operationName);

            if (amount.HasValue)
                vm.Amount.ShouldBe(amount.Value);
            else
                vm.Amount.ShouldBe(0);
        }
    }
}
