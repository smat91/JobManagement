using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CustomerViewCommand { get; set; }

        public RelayCommand ArticleViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }


        public HomeViewModel HomeVM{get; set;}
        public CustomerViewModel CustomerVM { get; set; }
        public ArticleViewModel ArticleVM { get; set; }
        public OrderViewModel OrderVM { get; set; }

        private object _currentView;
       
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CustomerVM = new CustomerViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o => 
            { 
                CurrentView = HomeVM;
            });

            CustomerViewCommand = new RelayCommand(o =>
            {
                CurrentView = CustomerVM;
            });
            ArticleViewCommand = new RelayCommand(o =>
            {
                CurrentView = ArticleVM;
            });
            OrderViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrderVM;
            });
        }
   
    }
}
