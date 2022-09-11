using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
        public string PasswordOld
        {
            get
            {
                return passwordOld_;
            }
            set
            {
                passwordOld_ = value;
                OnPropertyChanged();
            }
        }

        private int customerId_ = 0;
        private string passwordOld_ = "";
        private string passwordHash_ = "";

        public EditCustomerViewModel(CustomerConnection customerConnection) : base(customerConnection)
        {
            var id = MainViewModel.SelectedId;
            PasswordOld = "";         
            if (id > 0)
            {
                var customerTemp = customerConnection_.GetSingleById(MainViewModel.SelectedId);
                customerId_ = customerTemp.Id;
                CustomerNumber = customerTemp.CustomerNumber;
                Firstname = customerTemp.Firstname;
                Lastname = customerTemp.Lastname;
                EMail = customerTemp.EMail;
                passwordHash_ = customerTemp.Password;
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
                ProcessDataSet();
            }
            catch (ArgumentException e)
            {
                MessageBox.Show($"{e.Message}");
                return;
            }

            if (!(Password.IsNullOrEmpty() || PasswordRepeat.IsNullOrEmpty() || PasswordOld.IsNullOrEmpty()))
            { 
                customer_.Password = passwordOld_;
            }
            else
            {
                customer_.Password = passwordHash_;
            }

            customer_.Id = customerId_;
            customerConnection_.Update(customer_);
        }

        protected new void ProcessDataSet() {
            CheckDataCompleteness();
            CheckCustomerNumberFormat();
            CheckEmailFormat();
            CheckWebSiteUrlFormat();
            if (!(Password.IsNullOrEmpty() || PasswordRepeat.IsNullOrEmpty() || PasswordOld.IsNullOrEmpty()))
            {
                CheckPasswordFormat();
                GeneratePasswordHash();
                CheckPasswordEquality();
                CheckOldPassword();
            }
        }
        protected new void CheckDataCompleteness()
        {
            var allComplete = !customer_.Firstname.IsNullOrEmpty()
                   && !customer_.Lastname.IsNullOrEmpty()
                   && !customer_.EMail.IsNullOrEmpty()
                   && !customer_.Website.IsNullOrEmpty()
                   && !customer_.Address.Street.IsNullOrEmpty()
                   && !customer_.Address.StreetNumber.IsNullOrEmpty()
                   && !customer_.Address.Zip.IsNullOrEmpty()
                   && !customer_.Address.City.IsNullOrEmpty()
                   && !customer_.Address.Country.IsNullOrEmpty();

            if (!allComplete)
                throw new ArgumentException("Es wurden nicht alle notwendigen Felder ausgefüllt!");
        }

        protected new void CheckPasswordFormat()
        {
            if (Password.IsNullOrEmpty())
                return;

            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*\W).{8,}$");

            var password = Password;

            if (!regex.Match(password).Success)
                throw new ArgumentException("Ungültiges Passwort erkannt!");
        }

        protected void CheckOldPassword()
        {
            passwordOld_ = GetHashString(passwordOld_);
            if (passwordHash_ != passwordOld_)
                throw new ArgumentException("Altes Passwort stimmt nicht überein!");
        }
    }
}
