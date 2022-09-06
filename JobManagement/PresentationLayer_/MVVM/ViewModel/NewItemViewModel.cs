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
    class NewItemViewModel : ObservableObject
    {
        public string Name
        {
            get
            {
                return item_.Name;
            }
            set
            {
                item_.Name = value;
                OnPropertyChanged();
            }
        }

        public ItemGroupDto Group
        {
            get
            {
                return item_.Group;
            }
            set
            {
                item_.Group = value;
                OnPropertyChanged();
            }
        }

        public decimal Price
        {
            get
            {
                return item_.Price;

            }
            set
            {
                item_.Price = value;
                OnPropertyChanged();
            }
        }

        public decimal Vat
        {
            get
            {
                return item_.Vat;

            }
            set
            {
                item_.Vat = value;
                OnPropertyChanged();
            }
        }

        public List<ItemGroupDto> ItemGroupList { get; set; }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        

        public ItemDto item_;
        
        public NewItemViewModel()
        {
            item_ = new ItemDto();
            ItemGroup itemGroup = new ItemGroup(new ItemGroupRepository());
            ItemGroupList = itemGroup.GetAll();
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        public virtual void Save()
        {
            Item item = new Item(new ItemRepository());
            if (DataCheck())
            {
                item.Add(item_);
                Cancel();
            }
            else
            {
                MessageBox.Show("Artikeldaten unvollständig!");
            }
        }

        public void Cancel()
        {
            Name = ""; 
            Group = null;
            Price = 0;
            Vat = 0;
        }

        public bool DataCheck()
        {
            return !item_.Name.IsNullOrEmpty()
                   && (item_.Group != null)
                   && (item_.Price >= 0)
                   && (item_.Vat >= 0);
        }
    }
}
