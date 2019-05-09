using BlikPrismApp.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BlikPrismApp.ViewModels
{
    public class AccountPageViewModel : ViewModelBase
    {
        #region PROPS
        private string _username;
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        #endregion

        #region COMMANDS
        private DelegateCommand _getBlikCodeCommand;
        public DelegateCommand GetBlikCodeCommand => _getBlikCodeCommand
            ?? (_getBlikCodeCommand = new DelegateCommand(ExecuteGetBlikCodeCommand, CanExecuteGetBlikCodeCommand));
        #endregion

        public AccountPageViewModel(INavigationService navigationService) : base(navigationService) =>
            Title = "My Account";

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            IsBusy = true;

            Username = parameters.TryGetValue<string>(nameof(Username).ToLower(), out var username)
                ? username ?? string.Empty
                : string.Empty;

            if (string.IsNullOrWhiteSpace(Username))
                await NavigationService.NavigateAsync($"/{nameof(SignInPage)}");

            IsBusy = false;

            base.OnNavigatingTo(parameters);
        }

        private bool CanExecuteGetBlikCodeCommand() => !string.IsNullOrWhiteSpace(Username);

        private async void ExecuteGetBlikCodeCommand() => await NavigationService.NavigateAsync($"{nameof(BlikCodePage)}",
            new NavigationParameters($"{nameof(Username).ToLower()}={Username}"));
    }
}
