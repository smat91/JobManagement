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
            private set
            {
                customer_.Id = value;
                OnPropertyChanged();
            }
        }

        public EditCustomerViewModel() : base()
        {
            Customer customer = new Customer();

            if (MainViewModel.SelectedId > 0)
            {
                var customerTemp = customer.GetCustomerById(MainViewModel.SelectedId);
                CustomerNumber = customerTemp.Id;
                Firstname = customerTemp.Firstname;
                Lastname = customerTemp.Lastname;
                EMail = customerTemp.EMail;
                Password = customerTemp.Password;
                Website = customerTemp.Website;
                Street = customerTemp.Address.Street;
                StreetNumber = customerTemp.Address.StreetNumber;
                Zip = customerTemp.Address.Zip;
                Country = customerTemp.Address.Country;
                City = customerTemp.Address.City;
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
