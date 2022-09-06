using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataAccessConnection;
using PresentationLayer.Core;
using DataAccessLayer.Repositories;

namespace PresentationLayer.MVVM.ViewModel
{

    internal class SearchCustomerViewModel : CustomerViewModel
    {
        public SearchCustomerViewModel()
        {
            MainViewModel.ReloadSearchCustomerView = ReloadSearchData;
            CustomerDtoTable = new DataTable();
            Customer customer = new Customer(new CustomerRepository());
            AddHeaderData(CustomerDtoTable);
            AddRowData(CustomerDtoTable, customer.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            Customer customer = new Customer(new CustomerRepository());
            CustomerDtoTable.Clear();
            AddRowData(CustomerDtoTable, customer.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
