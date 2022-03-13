using System;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    public delegate void ReloadSearchView();

    class MainViewModel : ObservableObject
    {

        public RelayCommand NewCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CustomerViewCommand { get; set; }
        public RelayCommand ItemViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }

        public HomeViewModel HomeVM{get; set;}
        public CustomerViewModel CustomerVM { get; set; }
        public NewCustomerViewModel NewCustomerVM { get; set; }
        public EditCustomerViewModel EditCustomerVM { get; set; }
        public SearchCustomerViewModel SearchCustomerVM { get; set; }
        public ItemViewModel ItemVM { get; set; }
        public NewItemViewModel NewItemVM { get; set; }
        public EditItemViewModel EditItemVM { get; set; }
        public SearchItemViewModel SearchItemVM { get; set; }
        public OrderViewModel OrderVM { get; set; }
        public NewOrderViewModel NewOrderVM { get; set; }
        public EditOrderViewModel EditOrderVM { get; set; }
        public SearchOrderViewModel SearchOrderVM { get; set; }

        //public ItemGroupViewModel ItemGroupVM { get; set; }
        public NewItemGroupViewModel NewItemGroupVM { get; set; }
        public EditItemGroupViewModel EditItemGroupVM { get; set; }

        public static Action ReloadCustomerView { get ; set; }
        public static Action ReloadSearchCustomerView { get ; set; }
        public static Action ReloadItemView { get ; set; }
        public static Action ReloadSearchItemView { get ; set; }
        public static Action ReloadOrderView { get ; set; }
        public static Action ReloadSearchOrderView { get ; set; }

        public static string SearchTermStatic
        {
            get
            {
                return searchTerm_;
            }
            set
            {
                searchTerm_ = value;
            }
        }

        public string SearchTerm
        {
            get
            {
                return SearchTermStatic;
            }
            set
            {
                SearchTermStatic = value;
                OnPropertyChanged();
            }
        }
        
        public static int SelectedId
        {
            get
            {
                return selectedId_;
            }
            set
            {
                selectedId_ = value;
            }
        }

        private static string searchTerm_ = "";
        private static int selectedId_;
        private object currentView_;
        private RadioButtonState radioButtonsState_;
        private enum RadioButtonState
        {
            Home,
            Customer,
            Item,
            Order,
            ItemGroup,
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
            SearchCustomerVM = new SearchCustomerViewModel();
            NewCustomerVM = new NewCustomerViewModel();
            ItemVM = new ItemViewModel();
            NewItemVM = new NewItemViewModel();
            EditItemVM = new EditItemViewModel();
            SearchItemVM = new SearchItemViewModel();
            OrderVM = new OrderViewModel();
            NewOrderVM = new NewOrderViewModel();
            EditOrderVM = new EditOrderViewModel();
            SearchOrderVM = new SearchOrderViewModel();
            NewItemGroupVM = new NewItemGroupViewModel();
            EditItemGroupVM = new EditItemGroupViewModel();

            CurrentView = HomeVM;
            radioButtonsState_ = RadioButtonState.Home;

            NewCommand = new RelayCommand(o => OnNewCommand());
            EditCommand = new RelayCommand(o => OnEditCommand());
            DeleteCommand = new RelayCommand(o => OnDeleteCommand());
            SearchCommand = new RelayCommand(o => OnSearchCommand());

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

            ItemViewCommand = new RelayCommand(o =>
            {
                CurrentView = ItemVM;
                radioButtonsState_ = RadioButtonState.Item;
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

                case RadioButtonState.Item:
                    CurrentView = NewItemVM;
                    break;

                case RadioButtonState.Order:
                    CurrentView = NewOrderVM;
                    break;

                case RadioButtonState.ItemGroup:
                    CurrentView = NewItemGroupVM;
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

                case RadioButtonState.Item:
                    break;

                case RadioButtonState.Order:
                    break;

                case RadioButtonState.ItemGroup:
                    break;
            }
        }

        private void OnSearchCommand()
        {
            switch (radioButtonsState_)
            {
                case RadioButtonState.Home:
                    break;

                case RadioButtonState.Customer:
                    CurrentView = SearchCustomerVM;
                    ReloadSearchCustomerView();
                    break;

                case RadioButtonState.Item:
                    CurrentView = SearchItemVM;
                    ReloadSearchItemView();
                    break;

                case RadioButtonState.Order:
                    CurrentView = SearchOrderVM;
                    ReloadSearchOrderView();
                    break;
            }
        }

        private void OnDeleteCommand()
        {
            switch (radioButtonsState_)
            {
                case RadioButtonState.Home:
                    break;

                case RadioButtonState.Customer:
                    var customer = new Customer();
                    if (MessageBox.Show("Kunde endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = customer.DeleteCustomerByDto(
                            customer.GetCustomerById(selectedId_));

                        if (currentView_.GetType() == CustomerVM.GetType())
                        {
                            ReloadCustomerView();
                        }
                        else
                        {
                            ReloadSearchCustomerView();
                        }

                        MessageBox.Show(
                            res, 
                            "Info",
                            MessageBoxButton.OK, 
                            MessageBoxImage.Information);
                    }
                    break;

                case RadioButtonState.Item:
                    var item = new Item();
                    if (MessageBox.Show("Artikel endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = item.DeleteItemByDto(
                            item.GetItemById(selectedId_));

                        if (currentView_.GetType() == ItemVM.GetType())
                        {
                            ReloadItemView();
                        }
                        else
                        {
                            ReloadSearchItemView();
                        }

                        MessageBox.Show(
                            res,
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    break;

                case RadioButtonState.Order:
                    var order = new Order();
                    if (MessageBox.Show("Auftrag endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = order.DeleteOrderByDto(
                            order.GetOrderById(selectedId_));

                        if (currentView_.GetType() == OrderVM.GetType())
                        {
                            ReloadOrderView();
                        }
                        else
                        {
                            ReloadSearchOrderView();
                        }

                        MessageBox.Show(
                            res,
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    break;

                case RadioButtonState.ItemGroup:
                    var itemGroup = new ItemGroup();
                    if (MessageBox.Show("Artikelgruppe endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = itemGroup.DeleteItemGroupByDto(
                            itemGroup.GetItemGroupById(selectedId_));

                        ////if (currentView_.GetType() == ItemGroupVM.GetType())
                        //{
                        //    ReloadOrderView();
                        //}
                        //else
                        //{
                        //    ReloadSearchOrderView();
                        //}

                        //MessageBox.Show(
                        //    res,
                        //    "Info",
                        //    MessageBoxButton.OK,
                            //MessageBoxImage.Information);
                    }
                    break;
            }
        }
    }
}
