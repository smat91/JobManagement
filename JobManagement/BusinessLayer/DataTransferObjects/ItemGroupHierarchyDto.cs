using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.QueryTypes;

namespace BusinessLayer.DataTransferObjects
{
    public class ItemGroupHierarchyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentItemGroup { get; set; }

        public ItemGroupHierarchyDto()
        {
        }

        public ItemGroupHierarchyDto(ItemGroupHierarchyRequest itemGroup)
        {
            Id = itemGroup.Id;
            Name = itemGroup.Name;
            ParentItemGroup = itemGroup.ParentItemGroupId;
        }

        public static DataAccessLayer.QueryTypes.ItemGroupHierarchyRequest ItemGroupDtoToItemGroup(ItemGroupHierarchyDto itemGroup)
        {
            return new DataAccessLayer.QueryTypes.ItemGroupHierarchyRequest
            {
                Id = itemGroup.Id,
                Name = itemGroup.Name,
                ParentItemGroupId = itemGroup.ParentItemGroup
            };
        }

            public static List<ItemGroupHierarchyDto> ItemGroupListToItemGroupDtoList(List<DataAccessLayer.QueryTypes.ItemGroupHierarchyRequest> itemGroups)
        {
            List<ItemGroupHierarchyDto> itemGroupDtos = new List<ItemGroupHierarchyDto>();
            foreach (var itemGroup in itemGroups)
            {
                itemGroupDtos.Add(new ItemGroupHierarchyDto(itemGroup));
            }

            return itemGroupDtos;
        }
    }
}
