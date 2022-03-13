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
        public int ItemGroupNumber
        {
            get
            {
                return itemGroup_.Id;
            }
            private set
            {

            }
        }

        public EditItemGroupViewModel() : base()
        {
            ItemGroup itemGroup = new ItemGroup();

            if (MainViewModel.SelectedId > 0)
            {
                var itemGroupTemp = itemGroup.GetItemGroupById(MainViewModel.SelectedId);
                ItemGroupNumber = itemGroupTemp.Id;
                ParentItemGroup = itemGroupTemp.ParentItemGroup;
            }
        }

        public override void Save()
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
