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

    internal class NewCustomerViewModel : ObservableObject
    {
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
                return customer_.Password;
            } set
            {
                customer_.Password = value;
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
            } set
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
        

        public CustomerDto customer_;
       
     
       

        public NewCustomerViewModel()
        {
            customer_ = new CustomerDto();
            customer_.Address = new AddressDto();
            SaveCommand = new RelayCommand(o =>Save());
            CancleCommand = new RelayCommand(o => Cancel());

        }

        public virtual void Save()
        {
            Customer customer = new Customer();
            if (DataCheck())
            {
                customer.AddNewCustomer(customer_);
                Cancel();
            }
            else
            {
                MessageBox.Show("Es wurden nicht alle notwendigen Felder ausgefüllt!");
            }
        }

        public void Cancel()
        {
            Firstname = "";
            Lastname = "";
            EMail = "";
            Password = "";
            Website = "";
            Street = "";
            StreetNumber = "";
            Zip = "";
            City = "";
            Country = "";
        }

        public bool DataCheck()
        {
            // da kommt noch mehr.
            return !customer_.Firstname.IsNullOrEmpty()
                   && !customer_.Lastname.IsNullOrEmpty()
                   && !customer_.EMail.IsNullOrEmpty()
                   && !customer_.Website.IsNullOrEmpty()
                   && !customer_.Password.IsNullOrEmpty()
                   && !customer_.Address.Street.IsNullOrEmpty()
                   && !customer_.Address.StreetNumber.IsNullOrEmpty()
                   && !customer_.Address.Zip.IsNullOrEmpty()
                   && !customer_.Address.City.IsNullOrEmpty();
        }
    }
}
