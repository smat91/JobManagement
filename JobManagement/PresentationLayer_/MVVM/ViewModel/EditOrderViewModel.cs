﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PresentationLayer.Core;

namespace PresentationLayer.MVVM.ViewModel
{
    class EditOrderViewModel : NewOrderViewModel
    {
        public EditOrderViewModel() : base()
        {
            Order order = new Order();
            order_ = order.GetOrderById(MainViewModel.SelectedId);
        }

        private void Save()
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

