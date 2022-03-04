using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class ItemGroupConnection
    {
        private readonly ItemGroupRepository itemGroupRepository_;

        public ItemGroupConnection(string connectionString)
        {
            itemGroupRepository_ = new ItemGroupRepository(connectionString);
        }

        public IItemGroup GetItemGroupById(int id)
        {
            var itemGroup = itemGroupRepository_.GetItemGroupById(id);
            return itemGroup;
        }

        // @Matthias, stimmt das so?
        public Dictionary<string, int> GetItemsWithLevel()
        {
            return itemGroupRepository_.GetItemsWithLevel();
        }

        public void AddNewItemGroup(IItemGroup itemGroupDto)
        {
            itemGroupRepository_.AddNewItemGroup(itemGroupDto);
        }

        public void DeleteItemGroupByDto(IItemGroup itemGroupDto)
        {
            itemGroupRepository_.DeleteItemGroupByDto(itemGroupDto);
        }

        public void UpdateItemGroupByDto(IItemGroup itemGroupDto)
        {
            itemGroupRepository_.UpdateItemGroupByDto(itemGroupDto);
        }
    }
}
