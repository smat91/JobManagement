using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataAccessConnection;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using PresentationLayer.Annotations;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{

    internal class SearchArticleViewModel : ArticleViewModel
    {
        public SearchArticleViewModel()
        {
            MainViewModel.ReloadSearchArticleView = ReloadSearchData;
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
