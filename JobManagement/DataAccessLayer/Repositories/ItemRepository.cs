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
        private static string ConnectionString { get; set; }

        public ItemRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IItem GetItemById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var item = context.Items.Find(id);
                return item;
            }
        }

        public void AddNewItem(IItem item)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Items.Add((Item)item);
                context.SaveChanges();
            }
        }

        public void DeleteItemByDto(IItem item)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Items.Remove((Item)item);
                context.SaveChanges();
            }
        }

        public void UpdateItemByDto(IItem item)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Items.Update((Item)item);
                context.SaveChanges();
            }
        }
    }
}
