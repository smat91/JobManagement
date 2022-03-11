using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Repositories;

namespace BusinessLayer.DataAccessConnection
{
    public class Item
    {
        private readonly ItemRepository itemRepository_;

        public Item()
        {
            itemRepository_ = new ItemRepository();
        }

        public ItemDto GetItemById(int id)
        {
            var item = itemRepository_.GetItemById(id);
            return new ItemDto(item);
        }

        public List<ItemDto> GetItemsBySearchTerm(string searchTerm)
        {
            var items = itemRepository_.GetItemBySearchTerm(searchTerm);
            return ItemDto.ItemListToItemDtoList(items);
        }

        public List<ItemDto> GetAllItems()
        {
            var items = itemRepository_.GetAllItems();
            return ItemDto.ItemListToItemDtoList(items);
        }

        public void AddNewItem(ItemDto item)
        {
            itemRepository_.AddNewItem(ItemDto.ItemDtoToItem(item));
        }

        public string DeleteItemByDto(ItemDto item)
        {
            return itemRepository_.DeleteItemByDto(ItemDto.ItemDtoToItem(item));
        }

        public void UpdateItemByDto(ItemDto item)
        {
            itemRepository_.UpdateItemByDto(ItemDto.ItemDtoToItem(item));
        }
    }
}
