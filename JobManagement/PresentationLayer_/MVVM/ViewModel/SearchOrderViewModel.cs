﻿using System;
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
        public SearchOrderViewModel()
        {
            MainViewModel.ReloadSearchOrderView = ReloadSearchData;
            OrderDtoTable = new DataTable();
            OrderConnection order = new OrderConnection(new OrderRepository());
            AddHeaderData(OrderDtoTable);
            AddRowData(OrderDtoTable, order.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            OrderConnection order = new OrderConnection(new OrderRepository());
            OrderDtoTable.Clear();
            AddRowData(OrderDtoTable, order.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
