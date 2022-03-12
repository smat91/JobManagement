using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class NewItemGroupViewModel : ObservableObject
    {
        public string Name
        {
            get
            {
                return itemGroup_.Name;
            }
            set
            {
                itemGroup_.Name = value;
                OnPropertyChanged();
            }
        }

        public ItemGroupDto ParentItemGroup
        {
            get
            {
                return itemGroup_.ParentItemGroup;
            }
            set
            {
                itemGroup_.ParentItemGroup = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        

        private ItemGroupDto itemGroup_;
        
        public NewItemGroupViewModel()
        {
            itemGroup_ = new ItemGroupDto();
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        private void Save()
        {
            ItemGroup itemGroup = new ItemGroup();
            if (DataCheck())
            {
                itemGroup.AddNewItemGroup(itemGroup_);
                Cancel();
            }
            else
            {
                MessageBox.Show("Artikelgruppe unvollständig!");
            }
        }

        private void Cancel()
        {
            Name = ""; 
            ParentItemGroup = null;
        }

        private bool DataCheck()
        {
            return !itemGroup_.Name.IsNullOrEmpty()
                   && (itemGroup_.ParentItemGroup != null);
        }
    }
}