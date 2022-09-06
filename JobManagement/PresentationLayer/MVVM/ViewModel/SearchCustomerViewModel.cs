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
        public SearchCustomerViewModel(CustomerConnection customerConnection) : base(customerConnection)
        {
            MainViewModel.ReloadSearchCustomerView = ReloadSearchData;
            CustomerDtoTable = new DataTable();
            AddHeaderData(CustomerDtoTable);
            AddRowData(CustomerDtoTable, customerConnection_.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            CustomerDtoTable.Clear();
            AddRowData(CustomerDtoTable, customerConnection_.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
