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
            var item = itemRepository_.GetSingleById(id);
            return new ItemDto(item);
        }

        public List<ItemDto> GetItemsBySearchTerm(string searchTerm)
        {
            var items = itemRepository_.GetBySearchTerm(searchTerm);
            return ItemDto.ItemListToItemDtoList(items);
        }

        public List<ItemDto> GetAllItems()
        {
            var items = itemRepository_.GetAll();
            return ItemDto.ItemListToItemDtoList(items);
        }

        public void AddNewItem(ItemDto item)
        {
            itemRepository_.Add(ItemDto.ItemDtoToItem(item));
        }

        public string DeleteItemByDto(ItemDto item)
        {
            return itemRepository_.Delete(ItemDto.ItemDtoToItem(item));
        }

        public void UpdateItemByDto(ItemDto item)
        {
            itemRepository_.Update(ItemDto.ItemDtoToItem(item));
        }
    }
}
