using PresentationLayer.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Repositories;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class ItemGroupViewModel : ObservableObject
    {
        public ItemGroupTreeViewItem ItemDtoTable
        {
            get
            {
                return treeViewItem_;
            }
            set
            {
                treeViewItem_ = value;
                OnPropertyChanged();
            }
        }

        private ItemGroupTreeViewItem treeViewItem_;

        public ItemGroupViewModel () {
            ItemGroup itemGroup_ = new ItemGroup(new ItemGroupRepository());
            ItemDtoTable = ConvertData(itemGroup_.GetItemsWithLevel());
        }

        public ItemGroupTreeViewItem ConvertData(List<ItemGroupHierarchyDto> itemGroups)
        {
            Dictionary<int, ItemGroupTreeViewItem> helperDictionary = new Dictionary<int, ItemGroupTreeViewItem>();
            ItemGroupTreeViewItem itemGroupTreeViewItem = new ItemGroupTreeViewItem ();

            itemGroupTreeViewItem.Name = "Artikelgruppen";

            foreach (var itemGroup in itemGroups)
            {
                helperDictionary.Add(itemGroup.Id, new ItemGroupTreeViewItem
                {
                    ParentId = itemGroup.ParentItemGroup,
                    Name = itemGroup.Name,
                });
            }

            foreach (var itemGroup in helperDictionary)
            {
                if (itemGroup.Value.ParentId > 0)
                {
                    helperDictionary[itemGroup.Value.ParentId].Items.Add(itemGroup.Value);
                }
                else if (itemGroup.Value.ParentId == 0)
                {
                    itemGroupTreeViewItem.Items.Add(itemGroup.Value);
                }
            }

            return itemGroupTreeViewItem;
        }
    }

    public class ItemGroupTreeViewItem
    {
        public int ParentId { get; set; }

        public string Name { get; set; }
        public ObservableCollection<ItemGroupTreeViewItem> Items { get; set; }
        public ItemGroupTreeViewItem Parent { get; set; }

        public ItemGroupTreeViewItem()
        {
            this.Items = new ObservableCollection<ItemGroupTreeViewItem>();
        }
    }
}
