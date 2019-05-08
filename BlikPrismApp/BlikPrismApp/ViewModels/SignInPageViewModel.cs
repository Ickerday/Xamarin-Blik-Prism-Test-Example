using BlikPrismApp.Services.SignIn;
using BlikPrismApp.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using Xamarin.Forms;

namespace BlikPrismApp.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
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

        private bool _isInputEnabled;
        public bool IsInputEnabled { get => _isInputEnabled; set => SetProperty(ref _isInputEnabled, value); }
        #endregion

        #region COMMANDS
        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand => _loginCommand
            ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand, IsLoginInfoValid));
        #endregion

        public SignInPageViewModel(INavigationService navigationService, IPageDialogService dialogService,
            ISignInService signInService) : base(navigationService)
        {
            Title = "Sign in to account";
            IsInputEnabled = true;

            _dialogService = dialogService;
            _signInService = signInService;
        }

        private async void ExecuteLoginCommand()
        {
            IsLoginEnabled = IsInputEnabled = false;
            IsBusy = true;

            try
            {
                var userDto = new UserDto(Username, Password);
                var isSignedIn = await _signInService.SignInAsync(userDto);

                if (isSignedIn)
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(AccountPage)}",
                        new NavigationParameters($"{nameof(Username).ToLower()}={Username}"));
                else
                    throw new OperationCanceledException("Couldn't sign in.");
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Oops!", ex.Message, "Ok");
            }
            finally
            {
                IsLoginEnabled = IsInputEnabled = true;
                IsBusy = false;
            }
        }

        private bool IsLoginInfoValid() => !string.IsNullOrWhiteSpace(Password)
            && !string.IsNullOrWhiteSpace(Password);
    }
}