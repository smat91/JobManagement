using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using Castle.Core.Internal;
using DataAccessLayer.Repositories;
using PresentationLayer.Core;
using PresentationLayer.MVVM.ViewModel;

namespace PresentationLayer.MVVM.ViewModel
{
    internal class EditCustomerViewModel : NewCustomerViewModel
    {
        public EditCustomerViewModel(CustomerConnection customerConnection) : base(customerConnection)
        {
            var id = MainViewModel.SelectedId;
            
            if (id > 0)
            {
                var customerTemp = customerConnection_.GetSingleById(MainViewModel.SelectedId);
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

            customerConnection_.Update(customer_);
        }
    }
}
