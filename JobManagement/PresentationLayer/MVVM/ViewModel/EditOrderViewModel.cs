using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using Castle.Core.Internal;
using DataAccessLayer.Repositories;
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

        public EditOrderViewModel(ItemConnection itemConnection, CustomerConnection customerConnection, OrderConnection orderConnection) : base(itemConnection, customerConnection, orderConnection)
        {
            var id = MainViewModel.SelectedId;

            if (id > 0)
            {
                var orderTemp = orderConnection_.GetSingleById(id);
                OrderNumber = orderTemp.Id;
                Customer = orderTemp.Customer;
            }
        }

        public override void Save()
        {
            if (DataCheck())
            {
                orderConnection_.Update(order_);
            }
            else
            {
                MessageBox.Show("Einige Felder sind unvollständig\noder keine Positionen vorhanden!");
            }
        }

    }
}

