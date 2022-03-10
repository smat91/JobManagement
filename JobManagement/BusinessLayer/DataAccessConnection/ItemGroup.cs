using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
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

        public List<ItemGroupDto> GetItemGroupsBySearchTerm(string searchTerm)
        {
            var itemGroups = itemGroupRepository_.GetItemGroupBySearchTerm(searchTerm);
            return ItemGroupDto.ItemGroupListToItemGroupDtoList(itemGroups);
        }

        public List<ItemGroupDto> GetAllItemGroups()
        {
            var itemGroups = itemGroupRepository_.GetAllItemGroups();
            return ItemGroupDto.ItemGroupListToItemGroupDtoList(itemGroups);
        }

        public Dictionary<string, int> GetItemsWithLevel()
        {
            var itemGroups = itemGroupRepository_.GetItemsWithLevel();
            return itemGroups;
        }

        public void AddNewItemGroup(ItemGroupDto ItemGroup)
        {
            itemGroupRepository_.AddNewItemGroup(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }

        public void DeleteItemGroupByDto(ItemGroupDto ItemGroup)
        {
            itemGroupRepository_.DeleteItemGroupByDto(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }

        public void UpdateItemGroupByDto(ItemGroupDto ItemGroup)
        {
            itemGroupRepository_.UpdateItemGroupByDto(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }
    }
}
