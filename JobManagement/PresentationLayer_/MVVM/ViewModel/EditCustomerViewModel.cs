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
        public string CustomerNumber
        {
            get
            {
                return customer_.CustomerNumber;
            }
            set
            {
                customer_.CustomerNumber = value;
                OnPropertyChanged();
            }
        }

        public EditCustomerViewModel() : base()
        {
            Customer customer = new Customer();
            var id = 0;
            
            Int32.TryParse(MainViewModel.SelectedId, out id);

            if (id > 0)
            {
                var customerTemp = customer.GetCustomerByCustomerNumber(MainViewModel.SelectedId);
                CustomerNumber = customerTemp.CustomerNumber;
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
            try
            {
                CheckDataSet();
            }
            catch (ArgumentException e)
            {
                MessageBox.Show($"{e.Message}");
                return;
            }

            Customer customer = new Customer();
            customer.UpdateCustomerByDto(customer_);
        }
    }
}
