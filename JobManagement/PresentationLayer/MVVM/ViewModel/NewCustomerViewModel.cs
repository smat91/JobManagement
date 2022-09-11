using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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

    internal class NewCustomerViewModel : ObservableObject
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

        public string Firstname
        {
            get
            {
                return customer_.Firstname;
            }
            set
            {
                customer_.Firstname = value;
                OnPropertyChanged();
            }
        }

        public string Lastname
        {
            get
            {
                return customer_.Lastname;
            }
            set
            {
                customer_.Lastname = value;
                OnPropertyChanged();
            }
        }

        public string EMail
        {
            get
            {
                return customer_.EMail;
            }
            set
            {
                customer_.EMail = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return password_;
            } set
            {
                password_ = value;
                OnPropertyChanged();
            }
        }

        public string PasswordRepeat
        {
            get
            {
                return passwordRepeat_;
            }
            set
            {
                passwordRepeat_ = value;
                OnPropertyChanged();
            }
        }

        public string Website
        {
            get
            {
                return customer_.Website;
            }
            set
            {
                customer_.Website = value;
                OnPropertyChanged();
            }
        }
        
        public string Street
        {
            get
            {
                return customer_.Address.Street;
            }
            set
            {
                customer_.Address.Street = value;
                OnPropertyChanged();
            }
        }

        public string StreetNumber
        {
            get
            {
                return customer_.Address.StreetNumber;
            }
            set
            {
                customer_.Address.StreetNumber = value;
                OnPropertyChanged();
            }
        }

        public string Zip
        {
            get
            {
                return customer_.Address.Zip;
            }
            set
            {
                customer_.Address.Zip = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get
            {
                return customer_.Address.Country;
            } 
            set
            {
                customer_.Address.Country = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get
            {
                return customer_.Address.City;
            }
            set
            {
                customer_.Address.City = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancleCommand { get; set; }
        
        private string password_ = "";
        private string passwordRepeat_ = "";
            
        public CustomerDto customer_;

        protected CustomerConnection customerConnection_;
        public NewCustomerViewModel(CustomerConnection customerConnection)
        {
            customerConnection_ = customerConnection;
            customer_ = new CustomerDto();
            customer_.Address = new AddressDto();
            SaveCommand = new RelayCommand(o =>Save());
            CancleCommand = new RelayCommand(o => Cancel());

        }

        public virtual void Save()
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
            customer_.Password = password_;
            customerConnection_.Add(customer_);
            Cancel();
        }

        public void Cancel()
        {
            CustomerNumber = "";
            Firstname = "";
            Lastname = "";
            EMail = "";
            Password = "";
            PasswordRepeat = "";
            Website = "";
            Street = "";
            StreetNumber = "";
            Zip = "";
            City = "";
            Country = "";
        }

        protected void ProcessDataSet()
        {
            CheckDataCompleteness();
            CheckCustomerNumberFormat();
            CheckEmailFormat();
            CheckWebSiteUrlFormat();
            CheckPasswordFormat();
            GeneratePasswordHash();
            CheckPasswordEquality();
        }

        protected void CheckDataCompleteness()
        {
            var allComplete = !customer_.Firstname.IsNullOrEmpty()
                   && !customer_.Lastname.IsNullOrEmpty()
                   && !customer_.EMail.IsNullOrEmpty()
                   && !customer_.Website.IsNullOrEmpty()
                   && !customer_.Address.Street.IsNullOrEmpty()
                   && !customer_.Address.StreetNumber.IsNullOrEmpty()
                   && !Password.IsNullOrEmpty()
                   && !PasswordRepeat.IsNullOrEmpty()
                   && !customer_.Address.Zip.IsNullOrEmpty();

            if (!allComplete)
                throw new ArgumentException("Es wurden nicht alle notwendigen Felder ausgefüllt!");
        }

        protected void CheckCustomerNumberFormat()
        {
            Regex regex = new Regex(@"^CU[0-9]{5}$");

            var customerNumber = customer_.CustomerNumber;

            if (!regex.Match(customerNumber).Success)
                throw new ArgumentException("Ungültige Kundennummer erkannt!");
        }

        protected void CheckEmailFormat()
        {
            Regex regex = new Regex(@"^([a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)$");

            var email = customer_.EMail;

            if (!regex.Match(email).Success)
                throw new ArgumentException("Ungültige Emailadresse erkannt!");
        }

        protected void CheckWebSiteUrlFormat()
        {
            Regex regex = new Regex(@"^(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})$");

            var webSiteUrl = customer_.Website;

            if (!regex.Match(webSiteUrl).Success)
                throw new ArgumentException("Ungültige Webseiten Url erkannt!");
        }

        protected void CheckPasswordFormat()
        {
            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z])(?=.*\W).{8,}$");

            var password = Password;

            if (!regex.Match(password).Success)
                throw new ArgumentException("Ungültiges Passwort erkannt!");
        }

        protected void GeneratePasswordHash() {
            password_ = GetHashString(password_);
            passwordRepeat_ = GetHashString(passwordRepeat_);
        }

        protected void CheckPasswordEquality()
        {
            if (password_ != passwordRepeat_)
                throw new ArgumentException("Passwörter stimmen nicht überein!");
        }

        protected static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        protected static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
