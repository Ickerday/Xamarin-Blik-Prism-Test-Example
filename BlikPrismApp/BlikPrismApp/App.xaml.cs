using BlikPrismApp.Services.Blik;
using BlikPrismApp.Services.SignIn;
using BlikPrismApp.ViewModels;
using BlikPrismApp.Views;
using Prism;
using Prism.Ioc;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BlikPrismApp
{
    public partial class App
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            try
            {
                InitializeComponent();

                await NavigationService.NavigateAsync($"/{nameof(SignInPage)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override async void OnResume()
        {
            base.OnResume();

            await NavigationService.NavigateAsync($"/{nameof(SignInPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            #region PAGES
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();

            containerRegistry.RegisterForNavigation<BlikCodePage, BlikCodePageViewModel>();
            containerRegistry.RegisterForNavigation<BlikConfirmationPage, BlikConfirmationPageViewModel>();
            #endregion

            #region SERVICES
            containerRegistry.Register<ISignInService, SignInService>();
            containerRegistry.Register<IBlikService, BlikService>();
            #endregion
        }
    }
}
