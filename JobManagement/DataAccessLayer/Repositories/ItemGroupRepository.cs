using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class ItemGroupRepository
    {
        private static string connectionString_;

        public ItemGroupRepository(string connectionString)
        {
            connectionString_ = connectionString;
        }

        public IItemGroup GetItemGroupById(int id)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var itemGroup = context.ItemGroups.Find(id);
                return itemGroup;
            }
        }

        public Dictionary<string, int> GetItemsWithLevel()
        {
            using (var context = new JobManagementContext(connectionString_))
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
                        SELECT	[Name] ,
		                        [ProductLevel]
                        FROM [CTE_Products]
                        ORDER BY [ProductLevel], [Name];
                        "
                    )
                    .ToDictionary(res => res.Name, res => res.ProductLevel);
            }
        }

        public void AddNewItemGroup(IItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.ItemGroups.Add((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }

        public void DeleteItemGroupByDto(IItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.ItemGroups.Remove((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }

        public void UpdateItemGroupByDto(IItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.ItemGroups.Update((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }
    }
}
