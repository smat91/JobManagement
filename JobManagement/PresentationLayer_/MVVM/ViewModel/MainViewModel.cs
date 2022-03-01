using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM(get; set;)
        private object _currentView;
        public object CurrentView
        {
            get { return CurrentView; }
            set
            {
                CurrentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
        }
   
    }
}
