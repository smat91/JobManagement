using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ItemRepository
    {
        public IItem GetItemById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var item = context.Items.Find(id);
                context.Entry(item).Reference(i => i.Group).Load();

                return item;
            }
        }

        public List<IItem> GetAllItems()
        {
            using (var context = new JobManagementContext())
            {
                List<IItem> itemsList = new List<IItem>();

                context.Items
                    .Include(i => i.Group)
                    .ToList()
                    .ForEach(item => itemsList.Add(item));

                return itemsList;
            }
        }

        public void AddNewItem(IItem item)
        {
            using (var context = new JobManagementContext())
            {
                if (item.Group != null)
                {
                    var itemGroup = context.ItemGroups
                        .Find(item.Group.Id);
                    if (itemGroup != null)
                        item.Group = itemGroup;
                }

                context.Items.Add((Item)item);
                context.SaveChanges();
            }
        }

        public void DeleteItemByDto(IItem item)
        {
            using (var context = new JobManagementContext())
            {
                context.Items.Remove((Item)item);
                context.SaveChanges();
            }
        }

        public void UpdateItemByDto(IItem item)
        {
            using (var context = new JobManagementContext())
            {
                if (item.Group != null)
                {
                    var itemGroup = context.ItemGroups
                        .Find(item.Group.Id);
                    if (itemGroup != null)
                        item.Group = itemGroup;
                }

                context.Items.Update((Item)item);
                context.SaveChanges();
            }
        }
    }
}
