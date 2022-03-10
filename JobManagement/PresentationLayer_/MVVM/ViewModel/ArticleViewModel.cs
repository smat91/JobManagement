using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataAccessConnection;
using PresentationLayer.Annotations;

namespace PresentationLayer.MVVM.ViewModel
{

    internal class ArticleViewModel
    {
        public List<ItemDto> ItemDtoList { get; set; }

        public ArticleViewModel()
        {
            Item items = new Item();
            ItemDtoList = items.GetAllItems();
        }
    }
}
