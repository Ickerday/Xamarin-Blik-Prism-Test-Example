using BlikPrismApp.Services.Blik;
using BlikPrismApp.Services.SignIn;
using BlikPrismApp.Views;
using Prism.Navigation;
using Prism.Services;
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
        public int? BlikCode { get => _blikCode; set => SetProperty(ref _blikCode, value); }
        #endregion

        public BlikCodePageViewModel(INavigationService navigationService, IBlikService blikService,
            IPageDialogService dialogService) : base(navigationService)
        {
            _blikService = blikService;
            _dialogService = dialogService;
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            IsBusy = true;

            string username;
            username = parameters.TryGetValue(nameof(username), out username)
                ? username
                : string.Empty;

            if (string.IsNullOrWhiteSpace(username))
                await NavigationService.GoBackAsync();

            try
            {
                var userDto = new UserDto(username, string.Empty);
                BlikCode = await _blikService.GetBlikCodeAsync(userDto);
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("Oops!", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
                base.OnNavigatingTo(parameters);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (!BlikCode.HasValue)
            {
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
