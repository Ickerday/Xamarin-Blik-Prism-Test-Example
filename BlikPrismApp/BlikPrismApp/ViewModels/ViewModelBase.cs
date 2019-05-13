using Prism.Navigation;
using ReactiveUI;
using System.Threading.Tasks;

namespace BlikPrismApp.ViewModels
{
    public class ViewModelBase : ReactiveObject, INavigationAware, IDestructible
    {
        protected readonly INavigationService NavigationService;

        #region PROPS
        private string _title;
        public string Title { get => _title; set => this.RaiseAndSetIfChanged(ref _title, value); }

        private ObservableAsPropertyHelper<bool> _isBusy;
        public bool IsBusy => _isBusy.Value;
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
