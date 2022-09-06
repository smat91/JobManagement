﻿using System;
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

        public EditItemViewModel() : base()
        {
            Item item = new Item(new ItemRepository());
            var id = 0;

            Int32.TryParse(MainViewModel.SelectedId, out id);

            if (id > 0)
            {
                var itemTemp = item.GetSingleById(id);
                ItemNumber = itemTemp.Id;
                Name = itemTemp.Name;
                Group = itemTemp.Group;
                Price = itemTemp.Price;
                Vat = itemTemp.Vat;
            }
        }

        public override void Save()
        {
            Item item = new Item(new ItemRepository());
            if (DataCheck())
            {
                item.Update(item_);
            }
            else
            {
                MessageBox.Show("Artikeldaten unvollständig!");
            }
        }
    }
}
