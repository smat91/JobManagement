using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using Castle.Core.Internal;
using DataAccessLayer.Repositories;
using PresentationLayer.Core;
using PresentationLayer.MVVM.ViewModel;

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

        public EditItemGroupViewModel(ItemGroupConnection itemGroupConnection) : base(itemGroupConnection)
        {
            var id = MainViewModel.SelectedId;

            if (id > 0)
            {
                var itemGroupTemp = itemGroupConnection_.GetSingleById(id);
                ItemGroupNumber = itemGroupTemp.Id;
                ParentItemGroup = itemGroupTemp.ParentItemGroup;
            }
        }

        public override void Save()
        {
            if (DataCheck())
            {
                itemGroupConnection_.Update(itemGroup_);
                Cancel();
            }
            else
            {
                MessageBox.Show("Artikelgruppe unvollständig!");
            }
        }
    }
}
