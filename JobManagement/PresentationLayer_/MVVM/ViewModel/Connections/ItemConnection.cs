using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using PresentationLayer.MVVM.ViewModel.Models;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class ItemConnection
    {
        private static string connectionString_ = "";
        private readonly ItemRepository itemRepository_;

        public ItemConnection(string connectionString)
        {
            connectionString_ = connectionString;
            itemRepository_ = new ItemRepository(connectionString_);
        }

        public IItem GetItemById(int id)
        {
            var item = itemRepository_.GetItemById(id);
            return item;
        }

        public void AddNewItem(IItem item)
        {
            itemRepository_.AddNewItem(item);
        }

        public void DeleteItemByDto(IItem item)
        {
            itemRepository_.DeleteItemByDto(item);
        }

        public void UpdateItemByDto(IItem item)
        {
            itemRepository_.UpdateItemByDto(item);
        }
    }
}
