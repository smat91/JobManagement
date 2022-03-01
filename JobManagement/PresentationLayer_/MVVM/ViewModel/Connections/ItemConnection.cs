using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using PresentationLayer.MVVM.ViewModel.Models;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class ItemConnection
    {
        private readonly ItemRepository itemRepository_;

        public ItemConnection(string connectionString)
        {
            itemRepository_ = new ItemRepository(connectionString);
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
