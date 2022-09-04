using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using DataAccessLayer.Repositories.HeplerRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ItemGroupRepository : BaseRepository<ItemGroup>
    {
        public override string TableName => "ItemGroup";

        public ItemGroup GetSingleById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var itemGroup = context.ItemGroups.Find(id);
                context.Entry(itemGroup).Reference(i => i.ParentItemGroup).Load();
                return itemGroup;
            }
        }

        public List<ItemGroup> GetBySearchTerm(string searchTerm)
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

        public List<ItemGroup> GetAll()
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

        public List<ItemGroupHierarchyRequest> GetItemsWithLevel()
        {
            using (var context = new JobManagementContext())
            {
                return context.ItemGroupHierarchyRequest.FromSqlRaw(
                        @"
                        WITH [CTE_Products] (
                        [Id], [Name], [ParentItemGroupId])
                        AS (
                        SELECT	[Id],
		                        [Name] ,
		                        [ParentItemGroupId]
                        FROM [dbo].[ItemGroups]
                        WHERE [ParentItemGroupId] IS NULL
                        UNION ALL
                        SELECT	[pn].[Id],
		                        [pn].[Name] ,
		                        [pn].[ParentItemGroupId]
                        FROM [dbo].[ItemGroups] AS [pn]
                        INNER JOIN [CTE_Products] AS [p1]
	                        ON [p1].[Id] = [pn].[ParentItemGroupId]
                        )

                        SELECT DISTINCT
	                        [Id] ,
	                        [Name] ,
	                        COALESCE([ParentItemGroupId], 0) AS ParentItemGroupId
                        FROM [CTE_Products];
                        "
                    )
                    .ToList();
            }
        }

        public void Add(ItemGroup itemGroupDto)
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

        public void Update(ItemGroup itemGroupDto)
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
