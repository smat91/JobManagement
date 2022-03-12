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
        public EditArticleViewModel() : base()
        {
            Item item = new Item();

            if (MainViewModel.SelectedId > 0)
            {
                item_ = item.GetItemById(MainViewModel.SelectedId);
            }
        }

        private void Save()
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
