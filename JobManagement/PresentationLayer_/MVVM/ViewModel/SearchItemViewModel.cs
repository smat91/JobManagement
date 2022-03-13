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

    internal class SearchItemViewModel : ItemViewModel
    {
        public SearchItemViewModel()
        {
            MainViewModel.ReloadSearchItemView = ReloadSearchData;
            ItemDtoTable = new DataTable();
            Item items = new Item();
            AddHeaderData(ItemDtoTable);
            AddRowData(ItemDtoTable, items.GetItemsBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            Item items = new Item();
            ItemDtoTable.Clear();
            AddRowData(ItemDtoTable, items.GetItemsBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
