using BlikPrismApp.Services.Blik;
using BlikPrismApp.Services.SignIn;
using BlikPrismApp.Views;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using System;
using Xamarin.Forms;

namespace BlikPrismApp.ViewModels
{
    public class BlikCodePageViewModel : ViewModelBase
    {
        #region SERVICES
        private readonly IBlikService _blikService;
        private readonly IPageDialogService _dialogService;
        #endregion

        #region PROPS
        private int? _blikCode = null;
        public int? BlikCode { get => _blikCode; set => this.RaiseAndSetIfChanged(ref _blikCode, value); }
        #endregion

        public BlikCodePageViewModel(INavigationService navigationService, IBlikService blikService,
            IPageDialogService dialogService) : base(navigationService)
        {
            _blikService = blikService;
            _dialogService = dialogService;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            string username;
            username = parameters.TryGetValue(nameof(username), out username)
                ? username ?? string.Empty
                : string.Empty;

            if (string.IsNullOrWhiteSpace(username))
                await NavigationService.GoBackAsync();

            var userDto = new UserDto(username, string.Empty);

            var cmd = ReactiveCommand.CreateFromTask(async n => await _blikService.GetBlikCodeAsync(userDto));

            this.WhenAnyValue(vm => cmd.IsExecuting)
                .ToProperty(source: this, property: vm => vm.IsBusy);

            cmd.Subscribe(result => BlikCode = result);

            cmd.ThrownExceptions.Subscribe(async ex =>
                await _dialogService.DisplayAlertAsync("Oops!", ex.Message, "Ok"));

            base.OnNavigatingTo(parameters);
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!BlikCode.HasValue)
            {
                await NavigationService.GoBackAsync();
                base.OnNavigatedTo(parameters);
                return;
            }
            var paymentData = new NavigationParameters
            {
                { "operationName", "Majątek" },
                { "recipient", "Lichwa2000" },
                { "amount", 10 }
            };
            Device.StartTimer(TimeSpan.FromSeconds(15), () =>
            {
                NavigationService.NavigateAsync($"{nameof(BlikConfirmationPage)}", paymentData);
                return false;
            });
            base.OnNavigatedTo(parameters);
        }
    }
}
