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

        public List<ItemGroupDto> ItemGroupList { get; set; }

        public ItemGroupDto itemGroup_;
        protected ItemGroupConnection itemGroupConnection_;
        public NewItemGroupViewModel(ItemGroupConnection itemGroupConnection)
        {
            this.itemGroupConnection_ = itemGroupConnection;
            ItemGroupList = itemGroupConnection_.GetAll();
            itemGroup_ = new ItemGroupDto();
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        public virtual void Save()
        {
            if (DataCheck())
            {
                itemGroupConnection_.Add(itemGroup_);
                Cancel();
            }
            else
            {
                MessageBox.Show("Artikelgruppe unvollständig!");
            }
        }

        public void Cancel()
        {
            Name = ""; 
            ParentItemGroup = null;
        }

        public bool DataCheck()
        {
            return !itemGroup_.Name.IsNullOrEmpty();
        }
    }
}