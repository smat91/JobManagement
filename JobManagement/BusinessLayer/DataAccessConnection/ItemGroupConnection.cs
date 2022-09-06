using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.DataAccessConnection
{
    public class ItemGroupConnection : IItemGroupConnection
    {
        private readonly IItemGroupRepository itemGroupRepository_;

        public ItemGroupConnection(IItemGroupRepository itemGroupRepository)
        {
            itemGroupRepository_ = itemGroupRepository;
        }
        
        public ItemGroupDto GetSingleById(int id)
        {
            var itemGroup = itemGroupRepository_.GetSingleById(id);
            return new ItemGroupDto(itemGroup);
        }

        public List<ItemGroupDto> GetBySearchTerm(string searchTerm)
        {
            var itemGroups = itemGroupRepository_.GetBySearchTerm(searchTerm);
            return ItemGroupDto.ItemGroupListToItemGroupDtoList(itemGroups);
        }

        public List<ItemGroupDto> GetAll()
        {
            var itemGroups = itemGroupRepository_.GetAll();
            return ItemGroupDto.ItemGroupListToItemGroupDtoList(itemGroups);
        }

        public List<ItemGroupHierarchyDto> GetItemsWithLevel()
        {
            var itemGroupHierarchy = itemGroupRepository_.GetItemsWithLevel();
            return ItemGroupHierarchyDto.ItemGroupListToItemGroupDtoList(itemGroupHierarchy);
        }

        public void Add(ItemGroupDto ItemGroup)
        {
            itemGroupRepository_.Add(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }

        public string Delete(ItemGroupDto ItemGroup)
        {
            return itemGroupRepository_.Delete(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }

        public void Update(ItemGroupDto ItemGroup)
        {
            itemGroupRepository_.Update(ItemGroupDto.ItemGroupDtoToItemGroup(ItemGroup));
        }
    }
}
