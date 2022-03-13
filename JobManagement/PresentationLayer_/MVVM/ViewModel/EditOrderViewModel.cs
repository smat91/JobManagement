using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class EditOrderViewModel : NewOrderViewModel
    {
        public int OrderNumber
        {
            get
            {
                return order_.Id;
            } 
            private set
            {
                order_.Id = value;
                OnPropertyChanged();
            }
        }

        public EditOrderViewModel() : base()
        {
            Order order= new Order();

            if (MainViewModel.SelectedId > 0)
            {
                var orderTemp = order.GetOrderById(MainViewModel.SelectedId);
                OrderNumber = orderTemp.Id;
                Customer = orderTemp.Customer;
            }
        }

        public override void Save()
        {
            Order order = new Order();
            if (DataCheck())
            {
                order.UpdateOrderByDto(order_);
            }
            else
            {
                MessageBox.Show("Einige Felder sind unvollständig\noder keine Positionen vorhanden!");
            }
        }

    }
}

