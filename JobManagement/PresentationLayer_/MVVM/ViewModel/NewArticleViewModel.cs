using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class NewArticleViewModel : ObservableObject
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
        

        private ItemDto item_;
        
        public NewArticleViewModel()
        {
            item_ = new ItemDto();
            ItemGroup itemGroup = new ItemGroup();
            ItemGroupList = itemGroup.GetAllItemGroups();
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        private void Save()
        {
            Item item = new Item();
            if (DataCheck())
            {
                item.AddNewItem(item_);
            }
            else
            {
                MessageBox.Show("Artikeldaten unvollständig!");
            }
        }

        private void Cancel()
        {
            item_.Name = "";
            item_.Group = null;
            item_.Price = 0;
            item_.Vat = 0;
        }

        private bool DataCheck()
        {
            return !item_.Name.IsNullOrEmpty()
                   && (item_.Group != null)
                   && (item_.Price >= 0)
                   && (item_.Vat >= 0);
        }
    }
}
