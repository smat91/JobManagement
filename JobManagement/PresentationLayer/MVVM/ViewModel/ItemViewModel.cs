using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataAccessConnection;
using PresentationLayer.Core;
using DataAccessLayer.Repositories;

namespace PresentationLayer.MVVM.ViewModel
{

    internal class ItemViewModel : ObservableObject
    {
        public DataTable ItemDtoTable
        {
            get
            {
                return itemDtoTable_;
            }
            set
            {
                itemDtoTable_ = value;
                OnPropertyChanged();
            }
        }

        public DataRowView SelectedRow
        {
            get
            {
                return selectedRow_;
            }
            set
            {
                selectedRow_ = value;
                if (value != null)
                    MainViewModel.SelectedId = Int32.Parse(
                        value.Row[ItemDtoTable.Columns.IndexOf("Kundennummer")].ToString());
                OnPropertyChanged();
            }
        }

        private DataTable itemDtoTable_;
        private DataRowView selectedRow_;
        protected ItemConnection itemConnection_;
        public ItemViewModel(ItemConnection itemConnection)
        {
            this.itemConnection_ = itemConnection;
            MainViewModel.ReloadItemView = ReloadData;
            ItemDtoTable = new DataTable();
            AddHeaderData(ItemDtoTable);
            AddRowData(ItemDtoTable, itemConnection_.GetAll());
        }

        private void ReloadData()
        {
            ItemDtoTable.Clear();
            AddRowData(ItemDtoTable, itemConnection_.GetAll());
        }

        internal void AddHeaderData(DataTable dataTable)
        {
            // add header data
            dataTable.Columns.Add("Artikelnummer");
            dataTable.Columns.Add("Bezeichnung");
            dataTable.Columns.Add("Artikel Gruppe");
            dataTable.Columns.Add("Preis");
            dataTable.Columns.Add("MWSt");
        }

        internal void AddRowData(DataTable dataTable, List<ItemDto> itemDtoList)
        {
            foreach (var item in itemDtoList)
            {
                DataRow catRow = dataTable.NewRow();

                catRow["Artikelnummer"] = item.Id;
                catRow["Bezeichnung"] = item.Name;
                catRow["Artikel Gruppe"] = item.Group.Name;
                catRow["Preis"] = item.Price;
                catRow["MWSt"] = item.Vat;

                dataTable.Rows.Add(catRow);
            }
        }
    }
}
