using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;

namespace DataAccessLayer.Repositories
{
    public class ItemRepository
    {
        private static string ConnectionString { get; set; }

        public ItemRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ItemDto GetItemGroupById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var item = context.Items.Find(id);

                if (item != null)
                    return new ItemDto()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Group = item.Group,
                        Price = item.Price
                    };
                else
                {
                    return null;
                }
            }
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
