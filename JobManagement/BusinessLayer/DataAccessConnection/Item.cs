using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.DataAccessConnection
{
    public class Item : IItemConnection
    {
        private readonly IItemRepository itemRepository_;

        public Item(IItemRepository itemRepository)
        {
            itemRepository_ = itemRepository;
        }

        public ItemDto GetSingleById(int id)
        {
            var item = itemRepository_.GetSingleById(id);
            return new ItemDto(item);
        }

        public List<ItemDto> GetBySearchTerm(string searchTerm)
        {
            var items = itemRepository_.GetBySearchTerm(searchTerm);
            return ItemDto.ItemListToItemDtoList(items);
        }

        public List<ItemDto> GetAll()
        {
            var items = itemRepository_.GetAll();
            return ItemDto.ItemListToItemDtoList(items);
        }

        public void Add(ItemDto item)
        {
            itemRepository_.Add(ItemDto.ItemDtoToItem(item));
        }

        public string Delete(ItemDto item)
        {
            return itemRepository_.Delete(ItemDto.ItemDtoToItem(item));
        }

        public void Update(ItemDto item)
        {
            itemRepository_.Update(ItemDto.ItemDtoToItem(item));
        }
    }
}
