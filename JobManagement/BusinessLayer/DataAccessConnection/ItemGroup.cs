using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLayer.DataAccessConnection
{
    public class ItemGroup
    {
        private readonly ItemGroupRepository itemGroupRepository_;

        public ItemGroup()
        {
            itemGroupRepository_ = new ItemGroupRepository();
        }
        
        public ItemGroupDto GetItemGroupById(int id)
        {
            var itemGroup = itemGroupRepository_.GetItemGroupById(id);
            return (ItemGroupDto)itemGroup;
        }

        public List<ItemGroupDto> GetItemGroups()
        {
            var itemGroupsList = itemGroupRepository_.GetAllItemGroups();
            return itemGroupsList.ConvertAll(
                new Converter<IItemGroup, ItemGroupDto>(IItemGroupToItemGroupDto));
        }

        public Dictionary<string, int> GetItemsWithLevel()
        {
            var itemGroupList = itemGroupRepository_.GetItemsWithLevel();
            return itemGroupList;
        }

        public void AddNewItemGroup(ItemGroupDto itemGroupDto)
        {
            itemGroupRepository_.AddNewItemGroup(itemGroupDto);
        }

        public void DeleteItemGroupByDto(ItemGroupDto itemGroupDto)
        {
            itemGroupRepository_.DeleteItemGroupByDto(itemGroupDto);
        }

        public void UpdateItemGroupByDto(ItemGroupDto itemGroupDto)
        {
            itemGroupRepository_.UpdateItemGroupByDto(itemGroupDto);
        }

        private static ItemGroupDto IItemGroupToItemGroupDto(IItemGroup itemGroup)
        {
            return (ItemGroupDto)itemGroup;
        }
    }
}
