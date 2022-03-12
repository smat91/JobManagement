using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class EditItemGroupViewModel : NewItemGroupViewModel
    {

        public EditItemGroupViewModel()
        {
            ItemGroup itemGroup = new ItemGroup();
            itemGroup_ = itemGroup.GetItemGroupById(MainViewModel.SelectedId);
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        private void Save()
        {
            ItemGroup itemGroup = new ItemGroup();
            if (DataCheck())
            {
                itemGroup.UpdateItemGroupByDto(itemGroup_);
                Cancel();
            }
            else
            {
                MessageBox.Show("Artikelgruppe unvollständig!");
            }
        }
    }
}
