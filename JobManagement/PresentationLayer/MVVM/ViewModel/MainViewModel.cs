using System;
using System.Drawing;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.Interfaces;
using DataAccessLayer.Repositories;
using PresentationLayer.Core;
using PresentationLayer.MVVM.View;

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
        public RelayCommand ItemGroupViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }

        public HomeViewModel HomeViewModel {get; set;}
        public CustomerViewModel CustomerViewModel { get; set; }
        public NewCustomerViewModel NewCustomerViewModel { get; set; }
        public EditCustomerViewModel EditCustomerViewModel { get; set; }
        public SearchCustomerViewModel SearchCustomerViewModel { get; set; }
        public ItemViewModel ItemViewModel { get; set; }
        public NewItemViewModel NewItemViewModel { get; set; }
        public EditItemViewModel EditItemViewModel { get; set; }
        public SearchItemViewModel SearchItemViewModel { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
        public NewOrderViewModel NewOrderViewModel { get; set; }
        public EditOrderViewModel EditOrderViewModel { get; set; }
        public SearchOrderViewModel SearchOrderViewModel { get; set; }
        public ItemGroupViewModel ItemGroupViewModel { get; set; }
        public NewItemGroupViewModel NewItemGroupViewModel { get; set; }
        public EditItemGroupViewModel EditItemGroupViewModel { get; set; }

        public static Action ReloadCustomerView { get ; set; }
        public static Action ReloadSearchCustomerView { get ; set; }
        public static Action ReloadItemView { get ; set; }
        public static Action ReloadSearchItemView { get ; set; }
        public static Action ReloadOrderView { get ; set; }
        public static Action ReloadSearchOrderView { get ; set; }
        public static Action ReloadItemGroupView { get; set; }

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
        public System.Windows.Visibility TopBarVisibility
        {
            get
            {
                return topBarVisibility_;
            }
            set
            {
                topBarVisibility_ = value;
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
        private static System.Windows.Visibility topBarVisibility_ = Visibility.Hidden;
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

        private CustomerConnection customerConnection_;
        private OrderConnection orderConnection_;
        private ItemConnection itemConnection_;
        private ItemGroupConnection itemGroupConnection_;

        public object CurrentView
        {
            get { return currentView_; }
            set
            {
                currentView_ = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel(
            CustomerConnection customerConnection,
            OrderConnection orderConnection,
            ItemConnection itemConnection,
            ItemGroupConnection itemGroupConnection,
            HomeViewModel homeViewModel,
            CustomerViewModel customerViewModel,
            NewCustomerViewModel newCustomerViewModel,
            EditCustomerViewModel editCustomerViewModel,
            SearchCustomerViewModel searchCustomerViewModel,
            ItemViewModel itemViewModel,
            NewItemViewModel newItemViewModel,
            EditItemViewModel editItemViewModel,
            SearchItemViewModel searchItemViewModel,
            OrderViewModel orderViewModel,
            NewOrderViewModel newOrderViewModel,
            EditOrderViewModel editOrderViewModel,
            SearchOrderViewModel searchOrderViewModel,
            ItemGroupViewModel itemGroupViewModel,
            NewItemGroupViewModel newItemGroupViewModel,
            EditItemGroupViewModel editItemGroupViewModel)
        {
            HomeViewModel = homeViewModel;
            CustomerViewModel = customerViewModel;
            NewCustomerViewModel = newCustomerViewModel;
            EditCustomerViewModel = editCustomerViewModel;
            SearchCustomerViewModel = searchCustomerViewModel;
            ItemViewModel = itemViewModel;
            NewItemViewModel = newItemViewModel;
            EditItemViewModel = editItemViewModel;
            SearchItemViewModel = searchItemViewModel;
            OrderViewModel = orderViewModel;
            NewOrderViewModel = newOrderViewModel;
            EditOrderViewModel = editOrderViewModel;
            SearchOrderViewModel = searchOrderViewModel;
            ItemGroupViewModel = itemGroupViewModel;
            NewItemGroupViewModel = newItemGroupViewModel;
            EditItemGroupViewModel = editItemGroupViewModel;

            customerConnection_ = customerConnection;
            orderConnection_ = orderConnection;
            itemConnection_ = itemConnection;
            itemGroupConnection_ = itemGroupConnection;

            CurrentView = HomeViewModel;
            radioButtonsState_ = RadioButtonState.Home;

            NewCommand = new RelayCommand(o => OnNewCommand());
            EditCommand = new RelayCommand(o => OnEditCommand());
            DeleteCommand = new RelayCommand(o => OnDeleteCommand());
            SearchCommand = new RelayCommand(o => OnSearchCommand());

            HomeViewCommand = new RelayCommand(o => 
            { 
                CurrentView = HomeViewModel;
                TopBarVisibility = Visibility.Hidden;
                radioButtonsState_ = RadioButtonState.Home;
            });

            CustomerViewCommand = new RelayCommand(o =>
            {
                CurrentView = CustomerViewModel;
                TopBarVisibility = Visibility.Visible;
                radioButtonsState_ = RadioButtonState.Customer;
            });

            ItemViewCommand = new RelayCommand(o =>
            {
                CurrentView = ItemViewModel;
                TopBarVisibility = Visibility.Visible;
                radioButtonsState_ = RadioButtonState.Item;
            });

            ItemGroupViewCommand = new RelayCommand(o =>
            {
                CurrentView = ItemGroupViewModel;
                TopBarVisibility = Visibility.Visible;
                radioButtonsState_ = RadioButtonState.ItemGroup;
            });

            OrderViewCommand = new RelayCommand(o =>
            {
                CurrentView = OrderViewModel;
                TopBarVisibility = Visibility.Visible;
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
                    CurrentView = NewCustomerViewModel;
                    break;

                case RadioButtonState.Item:
                    CurrentView = NewItemViewModel;
                    break;

                case RadioButtonState.Order:
                    CurrentView = NewOrderViewModel;
                    break;

                case RadioButtonState.ItemGroup:
                    CurrentView = NewItemGroupViewModel;
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
                    CurrentView = EditCustomerViewModel;
                    break;

                case RadioButtonState.Item:
                    CurrentView = EditItemViewModel;
                    break;

                case RadioButtonState.Order:
                    CurrentView = EditOrderViewModel;
                    break;

                case RadioButtonState.ItemGroup:
                    CurrentView = EditItemGroupViewModel;
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
                    CurrentView = SearchCustomerViewModel;
                    ReloadSearchCustomerView();
                    break;

                case RadioButtonState.Item:
                    CurrentView = SearchItemViewModel;
                    ReloadSearchItemView();
                    break;

                case RadioButtonState.Order:
                    CurrentView = SearchOrderViewModel;
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
                    if (MessageBox.Show("Kunde endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = customerConnection_.Delete(
                            customerConnection_.GetSingleById(selectedId_));

                        if (currentView_.GetType() == CustomerViewModel.GetType())
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
                    if (MessageBox.Show("Artikel endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = itemConnection_.Delete(
                            itemConnection_.GetSingleById(selectedId_));

                        if (currentView_.GetType() == ItemViewModel.GetType())
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
                    if (MessageBox.Show("Auftrag endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = orderConnection_.Delete(
                            orderConnection_.GetSingleById(selectedId_));

                        if (currentView_.GetType() == OrderViewModel.GetType())
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
                    if (MessageBox.Show("Artikelgruppe endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = itemGroupConnection_.Delete(
                            itemGroupConnection_.GetSingleById(selectedId_));

                        ReloadItemGroupView();

                        MessageBox.Show(
                            res,
                            "Info",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    break;
            }
        }
    }
}
