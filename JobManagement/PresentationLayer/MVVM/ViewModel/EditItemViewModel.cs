using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using DataAccessLayer.Repositories;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class EditItemViewModel : NewItemViewModel
    {
        public int ItemNumber
        {
            get
            {
                return item_.Id;
            }
            private set
            {
                item_.Id = value;
                OnPropertyChanged();
            }
        }

        public EditItemViewModel(ItemConnection itemConnection, ItemGroupConnection itemGroupConnection) : base(itemConnection, itemGroupConnection)
        {
            var id = MainViewModel.SelectedId;

            if (id > 0)
            {
                var itemTemp = itemConnection_.GetSingleById(id);
                ItemNumber = itemTemp.Id;
                Name = itemTemp.Name;
                Group = itemTemp.Group;
                Price = itemTemp.Price;
                Vat = itemTemp.Vat;
            }
        }

        public override void Save()
        {
            if (DataCheck())
            {
                itemConnection_.Update(item_);
            }
            else
            {
                MessageBox.Show("Artikeldaten unvollständig!");
            }
        }
    }
}
