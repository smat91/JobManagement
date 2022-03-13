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

namespace PresentationLayer.MVVM.ViewModel
{

    internal class SearchCustomerViewModel : CustomerViewModel
    {
        public SearchCustomerViewModel()
        {
            MainViewModel.ReloadSearchCustomerView = ReloadSearchData;
            CustomerDtoTable = new DataTable();
            Customer customer = new Customer();
            AddHeaderData(CustomerDtoTable);
            AddRowData(CustomerDtoTable, customer.GetCustomersBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            Customer customer = new Customer();
            CustomerDtoTable.Clear();
            AddRowData(CustomerDtoTable, customer.GetCustomersBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
