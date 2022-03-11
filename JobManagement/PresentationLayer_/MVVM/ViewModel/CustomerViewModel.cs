using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class CustomerViewModel : ObservableObject
    {
        public DataTable CustomerDtoTable { get; set; }
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
                    value.Row[CustomerDtoTable.Columns.IndexOf("Kundennummer")].ToString());
                OnPropertyChanged();
            }
        }

        private DataRowView selectedRow_;
        
        public CustomerViewModel()
        {
            Customer customer = new Customer();
            CustomerDtoTable = new DataTable();
            AddHeaderData(CustomerDtoTable);
            AddRowData(CustomerDtoTable, customer.GetAllCustomers());
        }

        private void AddHeaderData(DataTable dataTable)
        {
            // add header data
            dataTable.Columns.Add("Kundennummer");
            dataTable.Columns.Add("Vorname");
            dataTable.Columns.Add("Nachname");
            dataTable.Columns.Add("E-Mail");
            dataTable.Columns.Add("Strasse");
            dataTable.Columns.Add("Ort");
        }

        private void AddRowData(DataTable dataTable, List<CustomerDto> customerDtoList)
        {
            foreach (var customer in customerDtoList)
            {
                DataRow catRow = dataTable.NewRow();

                catRow["Kundennummer"] = customer.Id;
                catRow["Vorname"] = customer.Firstname;
                catRow["Nachname"] = customer.Lastname;
                catRow["E-Mail"] = customer.EMail;
                catRow["Strasse"] = $"{customer.Address.Street} {customer.Address.StreetNumber}";
                catRow["Ort"] = $"{customer.Address.Zip} {customer.Address.City}";

                dataTable.Rows.Add(catRow);
            }
        }
    }
}
