using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using Castle.Core.Internal;
using PresentationLayer.Annotations;
using PresentationLayer.Core;
using PresentationLayer.MVVM.ViewModel;

namespace PresentationLayer.MVVM.ViewModel
{

    internal class EditCustomerViewModel : NewCustomerViewModel
    {
        public EditCustomerViewModel()
        {
            Customer customer = new Customer();
            customer_ = customer.GetCustomerById(MainViewModel.SelectedId);
            SaveCommand = new RelayCommand(o =>Save());
            CancleCommand = new RelayCommand(o =>Cancle());

        }

        private void Save()
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
