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

    internal class SearchOrderViewModel : OrderViewModel
    {
        public SearchOrderViewModel(OrderConnection orderConnection) : base(orderConnection)
        {
            MainViewModel.ReloadSearchOrderView = ReloadSearchData;
            OrderDtoTable = new DataTable();
            AddHeaderData(OrderDtoTable);
            AddRowData(OrderDtoTable, orderConnection_.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            OrderDtoTable.Clear();
            AddRowData(OrderDtoTable, orderConnection_.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
