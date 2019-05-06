using BlikPrismApp.Services.SignIn;
using BlikPrismApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using Xamarin.Forms;

namespace BlikPrismApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region SERVICES
        public readonly ISignInService _signInService;
        private readonly IPageDialogService _dialogService;
        #endregion

        #region PROPS
        private string _username = string.Empty;
        public string Username
        {
            get => _username; set
            {
                SetProperty(ref _username, value);
                IsLoginEnabled = IsLoginInfoValid();
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password; set
            {
                SetProperty(ref _password, value);
                IsLoginEnabled = IsLoginInfoValid();
            }
        }

        private bool _isLoginEnabled;
        public bool IsLoginEnabled { get => _isLoginEnabled; set => SetProperty(ref _isLoginEnabled, value); }
        #endregion

        #region COMMANDS
        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand, IsLoginInfoValid));
        #endregion

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            ISignInService signInService) : base(navigationService)
        {
            _dialogService = dialogService;
            _signInService = signInService;
        }

        private async void ExecuteLoginCommand()
        {
            try
            {
                //var isSignedIn = await _signInService.SignInAsync(Username, Password);

                if (true)
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
                else
                    throw new Exception("Couldn't sign in.");
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Oops!", ex.Message, "Ok");
            }
        }

        private bool IsLoginInfoValid() => !string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Password);
    }
}