using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.QueryTypes;
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
            var itemGroup = itemGroupRepository_.GetSingleById(id);
            return new ItemGroupDto(itemGroup);
        }

        public List<ItemGroupDto> GetItemGroupsBySearchTerm(string searchTerm)
        {
            var itemGroups = itemGroupRepository_.GetBySearchTerm(searchTerm);
            return ItemGroupDto.ItemGroupListToItemGroupDtoList(itemGroups);
        }

        public List<ItemGroupDto> GetAllItemGroups()
        {
            var itemGroups = itemGroupRepository_.GetAll();
            return ItemGroupDto.ItemGroupListToItemGroupDtoList(itemGroups);
        }

        public List<ItemGroupHierarchyDto> GetItemsWithLevel()
        {
            var itemGroupHierarchy = itemGroupRepository_.GetItemsWithLevel();
            return ItemGroupHierarchyDto.ItemGroupListToItemGroupDtoList(itemGroupHierarchy);
        }

        public void AddNewItemGroup(ItemGroupDto ItemGroup)
        {
            itemGroupRepository_.Add(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }

        public string DeleteItemGroupByDto(ItemGroupDto ItemGroup)
        {
            return itemGroupRepository_.Delete(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }

        public void UpdateItemGroupByDto(ItemGroupDto ItemGroup)
        {
            itemGroupRepository_.Update(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }
    }
}
