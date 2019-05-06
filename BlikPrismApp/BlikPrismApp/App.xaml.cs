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

                await NavigationService.NavigateAsync(nameof(LoginPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override async void OnResume()
        {
            base.OnResume();

            await NavigationService.NavigateAsync($"/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();

            containerRegistry.RegisterForNavigation<BlikCodePage, BlikCodePageViewModel>();
            containerRegistry.RegisterForNavigation<BlikConfirmationPage, BlikConfirmationPageViewModel>();

            containerRegistry.Register<ISignInService, SignInService>();
            containerRegistry.Register<IBlikService, BlikService>();
        }
    }
}
