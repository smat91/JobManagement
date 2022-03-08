using System.Collections.Generic;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class ItemConnection
    {
        public static IItem GetItemById(int id)
        {
            var item = ItemRepository.GetItemById(id);
            return item;
        }

        public static List<IItem> GetAllItems()
        {
            var itemsList = ItemRepository.GetAllItems();
            return itemsList;
        }

        public static void AddNewItem(IItem item)
        {
            ItemRepository.AddNewItem(item);
        }

        public static void DeleteItemByDto(IItem item)
        {
            ItemRepository.DeleteItemByDto(item);
        }

        public static void UpdateItemByDto(IItem item)
        {
            ItemRepository.UpdateItemByDto(item);
        }
    }
}
