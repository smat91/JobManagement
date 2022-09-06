using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using PresentationLayer.Core;
using DataAccessLayer.Repositories;
using PresentationLayer.MVVM.ViewModel;

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

                if (value != null)
                    MainViewModel.SelectedId = Int32.Parse(
                        value.Row[CustomerDtoTable.Columns.IndexOf("Id")].ToString());
                OnPropertyChanged();
            }
        }

        private DataRowView selectedRow_;
        protected CustomerConnection customerConnection_;

        public CustomerViewModel(CustomerConnection customerConnection)
        {
            MainViewModel.ReloadCustomerView = ReloadData;
            CustomerDtoTable = new DataTable();
            AddHeaderData(CustomerDtoTable);
            this.customerConnection_ = customerConnection;
            AddRowData(CustomerDtoTable, customerConnection_.GetAll());
        }

        private void ReloadData()
        {
            CustomerDtoTable.Clear();
            AddRowData(CustomerDtoTable, customerConnection_.GetAll());
        }

        public void AddHeaderData(DataTable dataTable)
        {
            // add header data
            dataTable.Columns.Add("Kundennummer");
            dataTable.Columns.Add("Vorname");
            dataTable.Columns.Add("Nachname");
            dataTable.Columns.Add("E-Mail");
            dataTable.Columns.Add("Strasse");
            dataTable.Columns.Add("Ort");
            dataTable.Columns.Add("Id");
        }

        public void AddRowData(DataTable dataTable, List<CustomerDto> customerDtoList)
        {
            foreach (var customer in customerDtoList)
            {
                DataRow catRow = dataTable.NewRow();

                catRow["Kundennummer"] = customer.CustomerNumber;
                catRow["Vorname"] = customer.Firstname;
                catRow["Nachname"] = customer.Lastname;
                catRow["E-Mail"] = customer.EMail;
                catRow["Strasse"] = $"{customer.Address.Street} {customer.Address.StreetNumber}";
                catRow["Ort"] = $"{customer.Address.Zip} {customer.Address.City}";
                catRow["Id"] = customer.Id;

                dataTable.Rows.Add(catRow);
            }
        }
    }
}
