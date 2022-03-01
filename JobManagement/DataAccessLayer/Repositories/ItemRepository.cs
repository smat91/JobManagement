using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class ItemRepository
    {
        private static string connectionString_;

        public ItemRepository(string connectionString)
        {
            connectionString_ = connectionString;
        }

        public IItem GetItemById(int id)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var item = context.Items.Find(id);
                return item;
            }
        }

        public void AddNewItem(IItem item)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Items.Add((Item)item);
                context.SaveChanges();
            }
        }

        public void DeleteItemByDto(IItem item)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Items.Remove((Item)item);
                context.SaveChanges();
            }
        }

        public void UpdateItemByDto(IItem item)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Items.Update((Item)item);
                context.SaveChanges();
            }
        }
    }
}
