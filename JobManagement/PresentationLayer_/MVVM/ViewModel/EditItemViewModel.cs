using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
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

        public EditItemViewModel() : base()
        {
            Item item = new Item();
            var id = Int32.Parse(MainViewModel.SelectedId);

            if (id > 0)
            {
                var itemTemp = item.GetItemById(id);
                ItemNumber = itemTemp.Id;
                Name = itemTemp.Name;
                Group = itemTemp.Group;
                Price = itemTemp.Price;
                Vat = itemTemp.Vat;
            }
        }

        public override void Save()
        {
            Item item = new Item();
            if (DataCheck())
            {
                item.UpdateItemByDto(item_);
            }
            else
            {
                MessageBox.Show("Artikeldaten unvollständig!");
            }
        }
    }
}
