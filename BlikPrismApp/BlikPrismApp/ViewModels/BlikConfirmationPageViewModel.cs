using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BlikPrismApp.ViewModels
{
    public class BlikConfirmationPageViewModel : ViewModelBase
    {
        #region SERVICES
        private readonly IPageDialogService _dialogService;
        #endregion

        #region PROPS
        private string _recipient;
        public string Recipient { get => _recipient; set => SetProperty(ref _recipient, value); }

        private string _operationName;
        public string OperationName { get => _operationName; set => SetProperty(ref _operationName, value); }

        private int _amount;
        public int Amount { get => _amount; set => SetProperty(ref _amount, value); }

        private string _pinCode = string.Empty;
        public string PinCode { get => _pinCode; set => SetProperty(ref _pinCode, value); }
        #endregion

        #region COMMANDS
        private DelegateCommand _confirmBlikCodeCommand;
        public DelegateCommand ConfirmBlikCode => _confirmBlikCodeCommand
            ?? (_confirmBlikCodeCommand = new DelegateCommand(ExecuteConfirmBlikCode));
        #endregion


        public BlikConfirmationPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService)
        {
            Title = "Confirm BLIK payment";

            _dialogService = dialogService;
        }

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

        private async void ExecuteConfirmBlikCode()
        {
            await _dialogService.DisplayAlertAsync("Yay!", "Operation confirmed", "Ok");

            await NavigationService.GoBackAsync();
            await NavigationService.GoBackAsync();
        }
    }
}
