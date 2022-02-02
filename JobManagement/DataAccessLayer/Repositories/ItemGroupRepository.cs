using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ItemGroupRepository
    {
        private static string ConnectionString { get; set; }

        public ItemGroupRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public ItemGroupDto GetItemGroupById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var itemGroup = context.ItemGroups.Find(id);

                if (itemGroup != null)
                    return new ItemGroupDto()
                    {
                        Id = itemGroup.Id,
                        Name = itemGroup.Name,
                        ParentItemGroup = (ItemGroupDto)itemGroup.ParentItemGroup
                    };
                else
                {
                    return null;
                }
            }
        }

        public  Dictionary<string, int> GetItemsWithLevel()
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                return context.ItemGroupHirarchy.FromSqlRaw(
                        @"WITH [CTE_Products] (
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
                        SELECT	[Name] ,
		                        [ProductLevel]
                        FROM [CTE_Products]
                        ORDER BY [ProductLevel], [Name];"
                    )
                    .ToDictionary(res => res.Name, res => res.ProductLevel);
            }
        }

        public void AddNewItemGroup(ItemGroupDto itemGroupDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.ItemGroups.Add(itemGroupDto);
                context.SaveChanges();
            }
        }

        public void DeleteItemGroupByDto(ItemGroupDto itemGroupDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.ItemGroups.Remove(itemGroupDto);
                context.SaveChanges();
            }
        }

        public void UpdateItemGroupByDto(ItemGroupDto itemGroupDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.ItemGroups.Update(itemGroupDto);
                context.SaveChanges();
            }
        }
    }
}
