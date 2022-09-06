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
        public SearchItemViewModel()
        {
            MainViewModel.ReloadSearchItemView = ReloadSearchData;
            ItemDtoTable = new DataTable();
            ItemConnection items = new ItemConnection(new ItemRepository());
            AddHeaderData(ItemDtoTable);
            AddRowData(ItemDtoTable, items.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }

        private void ReloadSearchData()
        {
            ItemConnection items = new ItemConnection(new ItemRepository());
            ItemDtoTable.Clear();
            AddRowData(ItemDtoTable, items.GetBySearchTerm(MainViewModel.SearchTermStatic));
        }
    }
}
