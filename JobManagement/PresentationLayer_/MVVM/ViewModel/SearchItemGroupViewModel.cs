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

    //internal class SearchItemGroupViewModel : ItemGroupViewModel
    //{
    //    public SearchItemGroupViewModel()
    //    {
    //        MainViewModel.ReloadSearchItemGroupView = ReloadSearchData;
    //        ItemGroupDtoTable = new DataTable();
    //        ItemGroup itemGroup = new ItemGroup();
    //        AddHeaderData(ItemGroupDtoTable);
    //        AddRowData(ItemGroupDtoTable, itemGroup.GetItemGroupsBySearchTerm(MainViewModel.SearchTermStatic));
    //    }

    //    private void ReloadSearchData()
    //    {
    //        ItemGroup itemGroup = new ItemGroup();
    //        ItemGroupDtoTable.Clear();
    //        AddRowData(ItemGroupDtoTable, itemGroup.GetItemGroupsBySearchTerm(MainViewModel.SearchTermStatic));
    //    }
    //}
}
