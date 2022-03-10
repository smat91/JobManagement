using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand NewCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CustomerViewCommand { get; set; }
        public RelayCommand ArticleViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }
        public RelayCommand NewArticleViewCommand { get; set; }

        public HomeViewModel HomeVM{get; set;}
        public CustomerViewModel CustomerVM { get; set; }
        public NewCustomerViewModel NewCustomerVM { get; set; }
        public ArticleViewModel ArticleVM { get; set; }
        public NewArticleViewModel NewArticleVM { get; set; }
        public OrderViewModel OrderVM { get; set; }

        private object currentView_;
        private RadioButtonState radioButtonsState_;
        private enum RadioButtonState
        {
            Home,
            Customer,
            Article,
            Order,
        }

        public object CurrentView
        {
            get { return currentView_; }
            set
            {
                currentView_ = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CustomerVM = new CustomerViewModel();
            NewCustomerVM = new NewCustomerViewModel();
            ArticleVM = new ArticleViewModel();
            NewArticleVM = new NewArticleViewModel();
            OrderVM = new OrderViewModel();

            CurrentView = HomeVM;
            radioButtonsState_ = RadioButtonState.Home;

            NewCommand = new RelayCommand(o => OnNewCommand());
            EditCommand = new RelayCommand(o => OnEditCommand());
            
            HomeViewCommand = new RelayCommand(o => 
            { 
                CurrentView = HomeVM;
                radioButtonsState_ = RadioButtonState.Home;
            });

            CustomerViewCommand = new RelayCommand(o =>
            {
                CurrentView = CustomerVM;
                radioButtonsState_ = RadioButtonState.Customer;
            });

            ArticleViewCommand = new RelayCommand(o =>
            {
                CurrentView = ArticleVM;
                radioButtonsState_ = RadioButtonState.Article;
            });

            OrderViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrderVM;
                radioButtonsState_ = RadioButtonState.Order;
            });
        }

        private void OnNewCommand()
        {
            switch (radioButtonsState_)
            {
                case RadioButtonState.Home:
                    break;

                case RadioButtonState.Customer:
                    CurrentView = NewCustomerVM;
                    break;

                case RadioButtonState.Article:
                    CurrentView = NewArticleVM;
                    break;

                case RadioButtonState.Order:
                    break;
            }
        }

        private void OnEditCommand()
        {
            switch (radioButtonsState_)
            {
                case RadioButtonState.Home:
                    break;

                case RadioButtonState.Customer:
                    break;

                case RadioButtonState.Article:
                    break;

                case RadioButtonState.Order:
                    break;
            }
        }
    }
}
