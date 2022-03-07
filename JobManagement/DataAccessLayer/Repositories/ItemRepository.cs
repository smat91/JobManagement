﻿using System;
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
        public static IItem GetItemById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var item = context.Items.Find(id);
                context.Entry(item).Reference(i => i.Group).Load();

                return item;
            }
        }

        public static void AddNewItem(IItem item)
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

        public static void DeleteItemByDto(IItem item)
        {
            using (var context = new JobManagementContext())
            {
                context.Items.Remove((Item)item);
                context.SaveChanges();
            }
        }

        public static void UpdateItemByDto(IItem item)
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
