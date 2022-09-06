using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Repositories;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class OrderViewModel : ObservableObject
    {
        public DataTable OrderDtoTable { get; set; }
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
                    MainViewModel.SelectedId = value.Row[OrderDtoTable.Columns.IndexOf("Auftragsnummer")].ToString();
                OnPropertyChanged();
            }
        }

        private DataRowView selectedRow_;

        public OrderViewModel()
        {
            MainViewModel.ReloadOrderView = ReloadData;
            OrderConnection order = new OrderConnection(new OrderRepository());
            OrderDtoTable = new DataTable();
            AddHeaderData(OrderDtoTable);
            AddRowData(OrderDtoTable, order.GetAll());
        }

        private void ReloadData()
        {
            OrderConnection order = new OrderConnection(new OrderRepository());
            OrderDtoTable.Clear();
            AddRowData(OrderDtoTable, order.GetAll());
        }

        public void AddHeaderData(DataTable dataTable)
        {
            // add header data
            dataTable.Columns.Add("Auftragsnummer");
            dataTable.Columns.Add("Kunde");
            dataTable.Columns.Add("Datum");
        }

        public void AddRowData(DataTable dataTable, List<OrderDto> orderDtoList)
        {
            foreach (var order in orderDtoList)
            {
                DataRow catRow = dataTable.NewRow();

                catRow["Auftragsnummer"] = order.Id;
                catRow["Kunde"] = $"{order.Customer.Firstname} {order.Customer.Lastname}";
                catRow["Datum"] = order.Date;

                dataTable.Rows.Add(catRow);
            }
        }
    }
}
