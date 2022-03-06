using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class ItemGroupConnection
    {
        public static IItemGroup GetItemGroupById(int id)
        {
            var itemGroup = ItemGroupRepository.GetItemGroupById(id);
            return itemGroup;
        }

        public static Dictionary<string, int> GetItemsWithLevel()
        {
            var itemGroupList = ItemGroupRepository.GetItemsWithLevel();
            return itemGroupList;
        }

        public static void AddNewItemGroup(IItemGroup itemGroupDto)
        {
            ItemGroupRepository.AddNewItemGroup(itemGroupDto);
        }

        public static void DeleteItemGroupByDto(IItemGroup itemGroupDto)
        {
            ItemGroupRepository.DeleteItemGroupByDto(itemGroupDto);
        }

        public static void UpdateItemGroupByDto(IItemGroup itemGroupDto)
        {
            ItemGroupRepository.UpdateItemGroupByDto(itemGroupDto);
        }
    }
}
