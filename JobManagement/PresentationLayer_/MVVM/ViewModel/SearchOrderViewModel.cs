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

    internal class SearchOrderViewModel : OrderViewModel
    {
        public SearchOrderViewModel()
        {
            MainViewModel.ReloadSearchOrderView = ReloadSearchData;
            OrderDtoTable = new DataTable();
            Order order = new Order();
            AddHeaderData(OrderDtoTable);
            AddRowData(OrderDtoTable, order.GetOrdersBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            Order order = new Order();
            OrderDtoTable.Clear();
            AddRowData(OrderDtoTable, order.GetOrdersBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
