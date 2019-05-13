using BlikPrismApp.Services.Blik;
using BlikPrismApp.Services.SignIn;
using Splat;

namespace BlikPrismApp
{
    public class AppBootstrapper : IEnableLogger
    {
        public AppBootstrapper() => RegisterDependencies();

        public void RegisterDependencies()
        {


            #region PAGES
            containerRegistry.RegisterForNavigation<AccountPage, AccountPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();

            containerRegistry.RegisterForNavigation<BlikCodePage, BlikCodePageViewModel>();
            containerRegistry.RegisterForNavigation<BlikConfirmationPage, BlikConfirmationPageViewModel>();
            #endregion

            #region SERVICES
            Locator.CurrentMutable.Register(() => new SignInService(), typeof(ISignInService));
            Locator.CurrentMutable.Register(() => new BlikService(), typeof(IBlikService));
            #endregion
        }
    }
}

