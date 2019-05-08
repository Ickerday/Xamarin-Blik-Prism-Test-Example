using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;

namespace BlikPrismApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected readonly INavigationService NavigationService;

        #region PROPS
        private string _title;
        public string Title { get => _title; set => SetProperty(ref _title, value); }

        private bool _isBusy;
        public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }
        #endregion

        public ViewModelBase(INavigationService navigationService) =>
            NavigationService = navigationService;

        public virtual async void OnNavigatedFrom(INavigationParameters parameters) =>
            await Task.CompletedTask;

        public virtual async void OnNavigatedTo(INavigationParameters parameters) =>
            await Task.CompletedTask;

        public virtual async void OnNavigatingTo(INavigationParameters parameters) =>
            await Task.CompletedTask;

        public virtual async void Destroy() =>
            await Task.CompletedTask;
    }
}
