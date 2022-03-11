using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataAccessConnection;
using PresentationLayer.Annotations;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{

    internal class ArticleViewModel : ObservableObject
    {
        public DataTable ItemDtoTable { get; set; }
        public DataRowView SelectedRow
        {
            get
            {
                return selectedRow_;
            }
            set
            {
                selectedRow_ = value;
                MainViewModel.SelectedId = Int32.Parse(
                    value.Row[ItemDtoTable.Columns.IndexOf("Artikelnummer")].ToString());
                OnPropertyChanged();
            }
        }

        private DataRowView selectedRow_;

        public ArticleViewModel()
        {
            Item items = new Item();
            ItemDtoTable = new DataTable();
            AddHeaderData(ItemDtoTable);
            AddRowData(ItemDtoTable, items.GetAllItems());
        }

        private void AddHeaderData(DataTable dataTable)
        {
            // add header data
            dataTable.Columns.Add("Artikelnummer");
            dataTable.Columns.Add("Bezeichnung");
            dataTable.Columns.Add("Artikel Gruppe");
            dataTable.Columns.Add("Preis");
            dataTable.Columns.Add("MWSt");
        }

        private void AddRowData(DataTable dataTable, List<ItemDto> itemDtoList)
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
