using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Annotations;
using PresentationLayer.Core;
using PresentationLayer.MVVM.ViewModel;

namespace PresentationLayer.MVVM.ViewModel
{

    internal class NewArticleViewModel : ObservableObject
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
        public RelayCommand CancleCommand { get; set; }
        

        private ItemDto item_;
        private MainViewModel mainViewModel_;
       

        public NewArticleViewModel(MainViewModel mainViewModel)
        {
            item_ = new ItemDto();
            mainViewModel_ = mainViewModel;
            ItemGroup itemGroup = new ItemGroup();
            ItemGroupList = itemGroup.GetItemGroups();
            SaveCommand = new RelayCommand(Save);

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

        private void Cancle()
        {
            mainViewModel_.LoadLastView();
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
