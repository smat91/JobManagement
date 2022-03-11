﻿using System;
using System.Dynamic;
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
        public RelayCommand ArticleViewCommand { get; set; }
        public RelayCommand OrderViewCommand { get; set; }

        public HomeViewModel HomeVM{get; set;}
        public CustomerViewModel CustomerVM { get; set; }
        public NewCustomerViewModel NewCustomerVM { get; set; }
        public SearchCustomerViewModel SearchCustomerVM { get; set; }
        public ArticleViewModel ArticleVM { get; set; }
        public NewArticleViewModel NewArticleVM { get; set; }
        public SearchArticleViewModel SearchArticleVM { get; set; }
        public OrderViewModel OrderVM { get; set; }
        public SearchOrderViewModel SearchOrderVM { get; set; }


        public static Action ReloadCustomerView { get ; set; }
        public static Action ReloadSearchCustomerView { get ; set; }
        public static Action ReloadArticleView { get ; set; }
        public static Action ReloadSearchArticleView { get ; set; }
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
            SearchCustomerVM = new SearchCustomerViewModel();
            NewCustomerVM = new NewCustomerViewModel();
            ArticleVM = new ArticleViewModel();
            NewArticleVM = new NewArticleViewModel();
            SearchArticleVM = new SearchArticleViewModel();
            OrderVM = new OrderViewModel();
            SearchOrderVM = new SearchOrderViewModel();

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

                case RadioButtonState.Article:
                    CurrentView = SearchArticleVM;
                    ReloadSearchArticleView();
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

                case RadioButtonState.Article:
                    var item = new Item();
                    if (MessageBox.Show("Kunde endgültig löschen?", "Warnung", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        var res = item.DeleteItemByDto(
                            item.GetItemById(selectedId_));

                        if (currentView_.GetType() == ArticleVM.GetType())
                        {
                            ReloadArticleView();
                        }
                        else
                        {
                            ReloadSearchArticleView();
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
            }
        }
    }
}
