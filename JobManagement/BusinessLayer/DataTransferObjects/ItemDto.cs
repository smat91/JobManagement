using System.Collections.Generic;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ItemGroupDto Group { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }

        public ItemDto()
        {
        }

        public ItemDto(IItem item)
        {
            Id = item.Id;
            Name = item.Name;
            Group = new ItemGroupDto(item.Group);
            Price = item.Price;
            Vat = item.Vat;
        }

        public static DataAccessLayer.Models.Item ItemDtoToItem(ItemDto item)
        {
            return new DataAccessLayer.Models.Item
            {
                Id = item.Id,
                Name = item.Name,
                Group = ItemGroupDto.ItemGroupDtoToItemGroup(item.Group),
                Price = item.Price,
                Vat = item.Vat
            };
        }

        public static List<ItemDto> ItemListToItemDtoList(List<IItem> items)
        {
            List<ItemDto> ItemDtos = new List<ItemDto>();
            foreach (var item in items)
            {
                ItemDtos.Add(new ItemDto(item));
            }

            return ItemDtos;
        }
    }
}
