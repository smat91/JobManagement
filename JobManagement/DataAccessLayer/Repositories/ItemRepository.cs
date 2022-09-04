using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ItemRepository : BaseRepository<Item>
    {
        public override string TableName => "Item";

        public Item GetSingleById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var item = context.Items
                    .Include(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .Single(item => item.Id == id);
                context.Entry(item).Reference(i => i.Group).Load();

                return item;
            }
        }

        public List<Item> GetBySearchTerm(string searchTerm)
        {
            List<Item> itemList = new List<Item>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.Items
                    .Include(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .AsEnumerable()
                    .Where(item => search.EvaluateSearchTerm(searchTerm, item))
                    .ToList()
                    .ForEach(item => itemList.Add(item));
            }

            return itemList;
        }

        public List<Item> GetAll()
        {
            using (var context = new JobManagementContext())
            {
                List<Item> itemsList = new List<Item>();

                context.Items
                    .Include(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .ToList()
                    .ForEach(item => itemsList.Add(item));

                return itemsList;
            }
        }

        public void Add(Item item)
        {
            using (var context = new JobManagementContext())
            {
                if (item.Group != null)
                {
                    var itemGroup = context.ItemGroups
                        .Include(group => group.ParentItemGroup)
                        .FirstOrDefault(group => group.Id == item.Group.Id);
                    if (itemGroup != default(ItemGroup))
                        item.Group = itemGroup;
                }

                context.Items.Add(item);
                context.SaveChanges();
            }
        }

        public void Update(Item item)
        {
            using (var context = new JobManagementContext())
            {
                if (item.Group != null)
                {
                    var itemGroup = context.ItemGroups
                        .Include(group => group.ParentItemGroup)
                        .FirstOrDefault(group => group.Id == item.Group.Id);
                    if (itemGroup != default(ItemGroup))
                        item.Group = itemGroup;
                }

                context.Items.Update(item);
                context.SaveChanges();
            }
        }
    }
}
