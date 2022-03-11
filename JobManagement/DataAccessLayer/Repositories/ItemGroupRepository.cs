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
    public class ItemGroupRepository
    {
        public ItemGroup GetItemGroupById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var itemGroup = context.ItemGroups.Find(id);
                context.Entry(itemGroup).Reference(i => i.ParentItemGroup).Load();
                return itemGroup;
            }
        }

        public List<ItemGroup> GetItemGroupBySearchTerm(string searchTerm)
        {
            List<ItemGroup> itemGroupList = new List<ItemGroup>();
            Search search = new Search();

            using (var context = new JobManagementContext())
            {
                context.ItemGroups
                    .Include(i => i.ParentItemGroup)
                    .AsEnumerable()
                    .Where(itemGroup => search.EvaluateSearchTerm(searchTerm, itemGroup))
                    .ToList()
                    .ForEach(itemGroup => itemGroupList.Add(itemGroup));
            }

            return itemGroupList;
        }

        public List<ItemGroup> GetAllItemGroups()
        {
            using (var context = new JobManagementContext())
            {
                List<ItemGroup> itemGroupsList = new List<ItemGroup>();

                context.ItemGroups
                    .Include(i => i.ParentItemGroup)
                    .ToList()
                    .ForEach(itemGroup => itemGroupsList.Add(itemGroup));

                return itemGroupsList;
            }
        }

        public Dictionary<string, int> GetItemsWithLevel()
        {
            using (var context = new JobManagementContext())
            {
                return context.ItemGroupHierarchyRequest.FromSqlRaw(
                        @"
                        WITH [CTE_Products] (
	                        [Id], [Name], [ParentItemGroupId], [ProductLevel] )
                        AS (
	                        SELECT	[Id],
			                        [Name] ,
		                            [ParentItemGroupId] ,
		                            0 AS [ProductLevel]
	                        FROM [dbo].[ItemGroups]
	                        WHERE [ParentItemGroupId] IS NULL
	                        UNION ALL
	                        SELECT	[pn].[Id],
			                        [pn].[Name] ,
		                            [pn].[ParentItemGroupId] ,
		                            [p1].[ProductLevel] + 1
	                        FROM [dbo].[ItemGroups] AS [pn]
	                        INNER JOIN [CTE_Products] AS [p1]
		                        ON [p1].[Id] = [pn].[ParentItemGroupId]
                        )
                        SELECT DISTINCT [Name] ,
		                        [ProductLevel]
                        FROM [CTE_Products]
                        ORDER BY [ProductLevel], [Name];
                        "
                    )
                    .ToDictionary(res => res.Name, res => res.ProductLevel);
            }
        }

        public void AddNewItemGroup(ItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext())
            {
                if (itemGroupDto.ParentItemGroup != null)
                {
                    var parentItemGroup = context.ItemGroups
                        .Include(itemGroup => itemGroup.ParentItemGroup)
                        .FirstOrDefault(itemGroup => itemGroup.Id == itemGroupDto.ParentItemGroup.Id);
                    if (parentItemGroup != default(ItemGroup))
                        itemGroupDto.ParentItemGroup = parentItemGroup;
                }

                context.ItemGroups.Add((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }

        public string DeleteItemGroupByDto(ItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext())
            {
                context.ItemGroups.Remove((ItemGroup)itemGroupDto);
                try
                {
                    context.SaveChanges();
                    return "Datensatz erfolgreich gelöscht";
                }
                catch (DbUpdateException e)
                {
                    return "Datensatz konnte nicht gelöscht werden.\nBitte zuerst Datensätze erntfernen in denen der Datensatz verwendet wird.";
                }
            }
        }

        public void UpdateItemGroupByDto(ItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext())
            {
                if (itemGroupDto.ParentItemGroup != null)
                {
                    var parentItemGroup = context.ItemGroups
                        .Include(itemGroup => itemGroup.ParentItemGroup)
                        .FirstOrDefault(itemGroup => itemGroup.Id == itemGroupDto.ParentItemGroup.Id);
                    if (parentItemGroup != default(ItemGroup))
                        itemGroupDto.ParentItemGroup = parentItemGroup;
                }

                context.ItemGroups.Update((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }
    }
}
