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

    internal class SearchItemViewModel : ItemViewModel
    {
        public SearchItemViewModel(ItemConnection itemConnection) : base(itemConnection)
        {
            MainViewModel.ReloadSearchItemView = ReloadSearchData;
            ItemDtoTable = new DataTable();
            AddHeaderData(ItemDtoTable);
            AddRowData(ItemDtoTable, itemConnection_.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            ItemDtoTable.Clear();
            AddRowData(ItemDtoTable, itemConnection_.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
