using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class EditArticleViewModel : NewArticleViewModel
    {
        public EditArticleViewModel()
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
    }
}
