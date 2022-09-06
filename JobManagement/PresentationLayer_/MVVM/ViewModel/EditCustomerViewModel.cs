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
using DataAccessLayer.Repositories;
using PresentationLayer.Core;
using PresentationLayer.MVVM.ViewModel;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class EditCustomerViewModel : NewCustomerViewModel
    {
        public EditCustomerViewModel() : base()
        {
            CustomerConnection customer = new CustomerConnection(new CustomerRepository());
            var id = 0;
            
            Int32.TryParse(MainViewModel.SelectedId, out id);

            if (id > 0)
            {
                var customerTemp = customer.GetSingleById(MainViewModel.SelectedId);
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

            CustomerConnection customer = new CustomerConnection(new CustomerRepository());
            customer.Update(customer_);
        }
    }
}
