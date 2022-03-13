using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Core;
using PresentationLayer.MVVM.ViewModel;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class EditCustomerViewModel : NewCustomerViewModel
    {
        public int CustomerNumber
        {
            get
            {
                return customer_.Id;
            }
        }

        public EditCustomerViewModel() : base()
        {
            Customer customer = new Customer();

            if (MainViewModel.SelectedId > 0)
            {
                customer_ = customer.GetCustomerById(MainViewModel.SelectedId);
            }

        }

        public override void Save()
        {
            Customer customer = new Customer();
            if (DataCheck())
            {
                customer.UpdateCustomerByDto(customer_);
            }
            else
            {
                MessageBox.Show("Es wurden nicht alle notwendigen Felder ausgefüllt!");
            }
        }
    }
}
