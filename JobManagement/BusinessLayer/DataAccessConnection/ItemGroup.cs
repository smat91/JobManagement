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
            return new ItemGroupDto(itemGroup);
        }

        public List<ItemGroupDto> GetItemGroups()
        {
            var itemGroupsList = itemGroupRepository_.GetAllItemGroups();
            return ItemGroupDto.ItemGroupListToItemGroupDtoList(itemGroupsList);
        }

        public Dictionary<string, int> GetItemsWithLevel()
        {
            var itemGroupList = itemGroupRepository_.GetItemsWithLevel();
            return itemGroupList;
        }

        public void AddNewItemGroup(ItemGroupDto IItemGroup)
        {
            itemGroupRepository_.AddNewItemGroup(ItemGroupDto.ItemGroupDtoToItemGroup(IItemGroup));
        }

        public void DeleteItemGroupByDto(ItemGroupDto IItemGroup)
        {
            itemGroupRepository_.DeleteItemGroupByDto(ItemGroupDto.ItemGroupDtoToItemGroup(IItemGroup));
        }

        public void UpdateItemGroupByDto(ItemGroupDto IItemGroup)
        {
            itemGroupRepository_.UpdateItemGroupByDto(ItemGroupDto.ItemGroupDtoToItemGroup(IItemGroup));
        }
    }
}
