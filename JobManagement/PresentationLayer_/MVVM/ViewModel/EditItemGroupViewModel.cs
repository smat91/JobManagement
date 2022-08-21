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
            var id = 0;

            Int32.TryParse(MainViewModel.SelectedId, out id);

            if (id > 0)
            {
                var itemGroupTemp = itemGroup.GetItemGroupById(id);
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
