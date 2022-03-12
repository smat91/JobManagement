using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class NewOrderViewModel : ObservableObject
    {
        // Bestellnummer fehlt
        // Kundennummer fehlt
        // Artikel, Bestelldatum, Position, Anzahl ist implementiert.
        
        public 
        
        public ItemDto Item
        {
            get
            {
                // da sollte doch Name kommen.
                return order_.Position.Item;
            }
            set
            {
                order_.Position.Item = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get
            {
                return order_.Position.Amount;
            }
            set
            {
                order_.Position.Amount = value;
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

        public ICollection<PositionDto> Positions
        {
            get
            {
                return order_.Positions;
            }
            set
            {
                order_.Positions = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }


        private OrderDto order_;

        public NewOrderViewModel()
        {
            order_ = new OrderDto();
            SaveCommand = new RelayCommand(o => Save());
            CancelCommand = new RelayCommand(o => Cancel());
        }

        private void Save()
        {
            Order order = new Order();
            if (DataCheck())
            {
                order.AddNewOrder(order_);
            }
            else
            {
                MessageBox.Show("Einige Felder sind unvollständig!");
            }
        }

        private void Cancel()
        {
            order_.Position.Item = null;
            //order_.Date = ;
            order_.Positions = null;
            order_.Position.Amount = 0;
        }

        private bool DataCheck()
        {
            // hier ist name drin
            return !order_.Position.Item.Name.IsNullOrEmpty()
                   //&& (order_.Date != "")
                   //&& (order_.Positions >= 0)
                   && (order_.Position.Amount >= 0);
        }
    }
}
