using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
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

        public EditOrderViewModel() : base()
        {
            OrderConnection order= new OrderConnection(new OrderRepository());
            var id = 0;

            Int32.TryParse(MainViewModel.SelectedId, out id);

            if (id > 0)
            {
                var orderTemp = order.GetSingleById(id);
                OrderNumber = orderTemp.Id;
                Customer = orderTemp.Customer;
            }
        }

        public override void Save()
        {
            OrderConnection order = new OrderConnection(new OrderRepository());
            if (DataCheck())
            {
                order.Update(order_);
            }
            else
            {
                MessageBox.Show("Einige Felder sind unvollständig\noder keine Positionen vorhanden!");
            }
        }

    }
}

