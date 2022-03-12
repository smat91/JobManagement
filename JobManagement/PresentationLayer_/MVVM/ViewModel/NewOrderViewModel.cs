using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Automation;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class NewOrderViewModel : ObservableObject
    {
        public CustomerDto Customer
        {
            get
            {
                return order_.Customer;
            }
            set
            {
                order_.Customer = value;
                OnPropertyChanged();
            }
        }

        public List<CustomerDto> CustomerList
        {
            get
            {
                return customerGroupList_;
            }
            set
            {
                customerGroupList_ = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get
            {
                return order_.Date;
            }
            set
            {
                order_.Date = value;
                OnPropertyChanged();
            }
        }

        public ItemDto Item {
            get
            {
                return item_;
            }
            set
            {
                item_ = value;
                OnPropertyChanged();
            }
        }

        public List<ItemDto> ItemList
        {
            get
            {
                Item item = new Item();
                return item.GetAllItems();
            }
            set
            {
                itemGroupList_ = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get
            {
                return amount_;
            }
            set
            {
                amount_ = value;
                OnPropertyChanged();
            }
        }

        public DataTable PositionTable
        {
            get
            {
                return positionTable_;
            }
            set
            {
                positionTable_ = value;
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
                OnPropertyChanged();
            }
        }


        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddPositionCommand { get; set; }
        public RelayCommand DeletePositionCommand { get; set; }

        public List<CustomerDto> customerGroupList_;
        public ItemDto item_;
        public List<ItemDto> itemGroupList_;
        public int amount_;
        public DataTable positionTable_;
        public DataRowView selectedRow_;
        public OrderDto order_;

        public NewOrderViewModel()
        {
            order_ = new OrderDto();
            order_.Positions = new List<PositionDto>();
            Date = DateTime.Now;

            Item item = new Item();
            ItemList = item.GetAllItems();

            Customer customer = new Customer();
            CustomerList = customer.GetAllCustomers();

            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
            AddPositionCommand = new RelayCommand(o => AddPosition());
            DeletePositionCommand = new RelayCommand(o => DeletePosition());

            PositionTable = new DataTable();
            AddHeaderData(PositionTable);
            AddRowData(PositionTable, order_.Positions);
        }

        public void Save()
        {
            Order order = new Order();
            if (DataCheck())
            {
                order.AddNewOrder(order_);
            }
            else
            {
                MessageBox.Show("Einige Felder sind unvollständig\noder keine Positionen vorhanden!");
            }
        }

        public void Cancel()
        {
            Customer = default(CustomerDto);
            Date = DateTime.Now;
            order_.Positions.Clear();
            ReloadData();
        }

        public bool DataCheck()
        {
            return Customer != default(CustomerDto)
                   && !order_.Positions.IsNullOrEmpty();
        }

        public void AddPosition()
        {
            if ((item_ != default(ItemDto)) && (amount_ > 0))
            {
                order_.Positions.Add(new PositionDto()
                {
                    Item = item_,
                    Amount = amount_
                });

                ReloadData();
            }
        }

        public void DeletePosition()
        {
            if (selectedRow_ != null)
            {
                var selectedName = selectedRow_.Row[PositionTable.Columns.IndexOf("Artikel")];
                var selectedAmount = Int32.Parse(
                    selectedRow_.Row[PositionTable.Columns.IndexOf("Menge")].ToString());

                PositionDto positionToDelete = null;

                foreach (var position in order_.Positions)
                {
                    if ((position.Item.Name == selectedName) && (position.Amount == selectedAmount))
                        positionToDelete = position; 
                }

                if (positionToDelete != null)
                    order_.Positions.Remove(positionToDelete);

                ReloadData();
            }
        }

        public void ReloadData()
        {
            PositionTable.Clear();
            AddRowData(PositionTable, order_.Positions);
        }

        internal void AddHeaderData(DataTable dataTable)
        {
            // add header data
            dataTable.Columns.Add("Artikel");
            dataTable.Columns.Add("Menge");
        }

        internal void AddRowData(DataTable dataTable, ICollection<PositionDto> positionCollection)
        {
            foreach (var position in positionCollection)
            {
                DataRow catRow = dataTable.NewRow();

                catRow["Artikel"] = position.Item.Name;
                catRow["Menge"] = position.Amount;

                dataTable.Rows.Add(catRow);
            }
        }
    }
}
