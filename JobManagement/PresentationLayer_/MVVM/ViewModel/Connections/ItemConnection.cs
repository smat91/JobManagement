using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Repositories;
using PresentationLayer.MVVM.ViewModel.Models;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class ItemConnection
    {
        private static string ConnectionString { get; set; }
        private readonly ItemRepository itemRepository_;

        public ItemConnection(string connectionString)
        {
            ConnectionString = connectionString;
            itemRepository_ = new ItemRepository(ConnectionString);
        }

        public Item GetItemById(int id)
        {
            var item = itemRepository_.GetItemById(id);

            return new Item()
            {
                Id = item.Id,
                Name = item.Name,
                Group = item.Group,
                Price = item.Price
            };
        }

        public void AddNewItem(ItemDto itemDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Items.Add(itemDto);
                context.SaveChanges();
            }
        }

        public void DeleteItemByDto(ItemDto itemDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Items.Remove(itemDto);
                context.SaveChanges();
            }
        }

        public void UpdateItemByDto(ItemDto itemDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Items.Update(itemDto);
                context.SaveChanges();
            }
        }
    }
}
