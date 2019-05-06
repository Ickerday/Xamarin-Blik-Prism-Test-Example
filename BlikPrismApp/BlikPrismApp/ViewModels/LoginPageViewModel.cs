using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace BlikPrismApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private string _username;
        public string Username { get => _username; set => SetProperty(ref _username, value); }

        private string _password;
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        //private bool _isEnabled;
        //public string IsEnabled { get => _isEnabled; set => SetProperty(ref _isEnabled, value); }
        public bool IsEnabled => string.IsNullOrWhiteSpace(Password) && string.IsNullOrWhiteSpace(Password);

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(async () => await ExecuteLoginCommand()));

        public LoginPageViewModel(INavigationService navigationService) : base(navigationService) { }

        private async Task ExecuteLoginCommand()
        {
            await Task.CompletedTask;


        }
    }
}
