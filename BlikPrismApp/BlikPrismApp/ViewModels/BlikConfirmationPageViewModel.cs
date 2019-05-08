using Prism.Navigation;

namespace BlikPrismApp.ViewModels
{
    public class BlikConfirmationPageViewModel : ViewModelBase
    {
        private string _recipient;
        public string Recipient { get => _recipient; set => SetProperty(ref _recipient, value); }

        private string _operationName;
        public string OperationName { get => _operationName; set => SetProperty(ref _operationName, value); }

        private int _amount;
        public int Amount { get => _amount; set => SetProperty(ref _amount, value); }

        public BlikConfirmationPageViewModel(INavigationService navigationService) : base(navigationService) =>
            Title = "Confirm BLIK payment";

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            string recipient;
            Recipient = parameters.TryGetValue(nameof(recipient), out recipient)
                ? recipient
                : string.Empty;

            string operationName;
            OperationName = parameters.TryGetValue(nameof(operationName), out operationName)
            ? operationName
            : string.Empty;

            int amount;
            Amount = parameters.TryGetValue(nameof(amount), out amount)
                ? amount
                : 0;

            base.OnNavigatingTo(parameters);
        }
    }
}
