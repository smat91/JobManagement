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
        public static IItemGroup GetItemGroupById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var itemGroup = context.ItemGroups.Find(id);
                return itemGroup;
            }
        }

        public static Dictionary<string, int> GetItemsWithLevel()
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
                        SELECT	[Name] ,
		                        [ProductLevel]
                        FROM [CTE_Products]
                        ORDER BY [ProductLevel], [Name];
                        "
                    )
                    .ToDictionary(res => res.Name, res => res.ProductLevel);
            }
        }

        public static void AddNewItemGroup(IItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext())
            {
                if (itemGroupDto.ParentItemGroup != null)
                {
                    var parentItemGroup = context.ItemGroups
                        .Find(itemGroupDto.ParentItemGroup.Id);
                    if (parentItemGroup != null)
                        itemGroupDto.ParentItemGroup = parentItemGroup;
                }

                context.ItemGroups.Add((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }

        public static void DeleteItemGroupByDto(IItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext())
            {
                context.ItemGroups.Remove((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }

        public static void UpdateItemGroupByDto(IItemGroup itemGroupDto)
        {
            using (var context = new JobManagementContext())
            {
                if (itemGroupDto.ParentItemGroup != null)
                {
                    var parentItemGroup = context.ItemGroups
                        .Find(itemGroupDto.ParentItemGroup.Id);
                    if (parentItemGroup != null)
                        itemGroupDto.ParentItemGroup = parentItemGroup;
                }

                context.ItemGroups.Update((ItemGroup)itemGroupDto);
                context.SaveChanges();
            }
        }
    }
}
