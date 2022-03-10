using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
{
    public class ItemGroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemGroupDto ParentItemGroup { get; set; }

        public ItemGroupDto()
        {
        }

        public ItemGroupDto(ItemGroup itemGroup)
        {
            Id = itemGroup.Id;
            Name = itemGroup.Name;
            ParentItemGroup = (itemGroup.ParentItemGroup == null) ? null : new ItemGroupDto(itemGroup.ParentItemGroup);
        }

        public static DataAccessLayer.Models.ItemGroup ItemGroupDtoToItemGroup(ItemGroupDto itemGroup)
        {
            return new DataAccessLayer.Models.ItemGroup
            {
                Id = itemGroup.Id,
                Name = itemGroup.Name,
                ParentItemGroup = (itemGroup.ParentItemGroup == null)? null : ItemGroupDto.ItemGroupDtoToItemGroup(itemGroup.ParentItemGroup)
            };
        }

        public static List<ItemGroupDto> ItemGroupListToItemGroupDtoList(List<ItemGroup> itemGroups)
        {
            List<ItemGroupDto> itemGroupDtos = new List<ItemGroupDto>();
            foreach (var itemGroup in itemGroups)
            {
                itemGroupDtos.Add(new ItemGroupDto(itemGroup));
            }

            return itemGroupDtos;
        }
    }
}
