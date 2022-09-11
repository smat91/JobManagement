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
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Windows;
using System.Linq.Expressions;

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

        public DateTime Date
        {
            get
            {
                return date_;
            }
            set
            {
                date_ = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ImoprtCommand { get; set; }
        public RelayCommand ExportXmlCommand { get; set; }
        public RelayCommand ExportJsonCommand { get; set; }

        private DataRowView selectedRow_;
        private DateTime date_;
        protected CustomerConnection customerConnection_;

        public CustomerViewModel(CustomerConnection customerConnection)
        {
            date_ = DateTime.Today;

            MainViewModel.ReloadCustomerView = ReloadData;
            CustomerDtoTable = new DataTable();
            AddHeaderData(CustomerDtoTable);
            this.customerConnection_ = customerConnection;
            AddRowData(CustomerDtoTable, customerConnection_.GetAll());

            ImoprtCommand = new RelayCommand(o => OnImoprtCommand());
            ExportXmlCommand = new RelayCommand(o => OnExportCommand("xml"));
            ExportJsonCommand = new RelayCommand(o => OnExportCommand("json"));
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

        private void ReloadData()
        {
            CustomerDtoTable.Clear();
            AddRowData(CustomerDtoTable, customerConnection_.GetAll());
        }

        private void OnImoprtCommand()
        {
            var openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "JSON Files (*.json)|*.json| XML Files (*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;

            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                var filePath = "";
                var fileType = "";
                var regex = new Regex(@"^.+\.(?'fileEnding'xml|json)$");

                filePath = openFileDialog.FileName;

                var regexMatch = regex.Match(filePath);

                if (!regexMatch.Success)
                {
                    MessageBox.Show($"Ungültiges Dateiformat erkannt!");
                    return;
                }
                else {
                    fileType = regexMatch.Groups["fileEnding"].Value;

                    var res = MessageBox.Show($"Vorhandene Einträge werden aktualisiert!\nImport fortsetzen?", "Import", MessageBoxButton.OKCancel);

                    if (res == MessageBoxResult.Cancel)
                        return;

                    try
                    {
                        customerConnection_.ImportCustomers(filePath, fileType);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Import fehlgeschlagen!\n{ex.Message}");
                        throw;
                    }
                }    
            }
        }

        private void OnExportCommand(string type)
        {
            var fileType = type;
            var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filePath += $"customers_export_{date_.ToString("ddMMyyyy")}.{fileType}";
            customerConnection_.ExportCustomers(filePath, fileType, date_);

            try
            {
                customerConnection_.ExportCustomers(filePath, fileType, date_);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export fehlgeschlagen!\n{ex.Message}");
                throw;
            }
        }
    }
}
