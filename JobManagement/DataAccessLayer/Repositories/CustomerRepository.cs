using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository
    {
        public enum Property
        {
            Id,
            Firstname,
            Lastname,
            EMail,
            Website,
            Street,
            StreetNumber,
            Zip,
            Country,
            City,
        }

        public Dictionary<Property, String> SearchTerm { get; set; }
        private static string connectionString_;

        public CustomerRepository(string connectionString)
        {
            connectionString_ = connectionString;
        }

        public ICustomer GetCustomerById(int id)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var customer = context.Customers.Find(id);
                return customer;
            }
        }

        public List<ICustomer> GetCustomersBySearchTerm(Dictionary<Property, String> searchTerm)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                List<ICustomer> customerList = new List<ICustomer>();

                context.Customers
                    .Where(customer => EvaluateSerachTerm(searchTerm, customer))
                    .ToList()
                    .ForEach(customer => customerList.Add(customer)
                    );

                return customerList;
            }
        }

        public List<ICustomer> GetAllCustomers()
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                List<ICustomer> customerList = new List<ICustomer>();
                    
                context.Customers.ToList()
                    .ForEach(customer => customerList.Add(customer));

                return customerList;
            }
        }

        public void AddNewCustomer(ICustomer customer)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Customers.Add((Customer)customer);
                context.SaveChanges();
            }
        }

        public void DeleteCustomerByDto(ICustomer customer)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Customers.Remove((Customer)customer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomerByDto(ICustomer customer)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                context.Customers.Update((Customer)customer);
                context.SaveChanges();
            }
        }

        public void SetAddressByCustomerDtoAndAddressDto(ICustomer customer, IAddress address)
        {
            using (var context = new JobManagementContext(connectionString_))
            {
                var customerTemp = context.Customers.Find(customer);
                var addressTemp = context.Addresses.Find(address);

                if (customerTemp == null)
                    return;

                if (addressTemp != null)
                    customerTemp.Address = addressTemp;
                else
                {
                    AddressRepository addressRepository = new AddressRepository(connectionString_);
                    addressRepository.AddNewAddress(address);
                    addressTemp = context.Addresses.Find(address);
                }

                customerTemp.Address = addressTemp;
            }
        }

        private bool EvaluateSerachTerm(Dictionary<Property, String> searchTerm, ICustomer customer)
        {
            bool result = true;

            result &= !searchTerm.ContainsKey(Property.Id) || 
                      searchTerm[Property.Id] == "" || 
                      customer.Id.ToString().Contains(searchTerm[Property.Id], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(Property.Firstname) ||
                      searchTerm[Property.Firstname] == "" ||
                      customer.Firstname.Contains(searchTerm[Property.Firstname], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(Property.Lastname) ||
                      searchTerm[Property.Lastname] == "" ||
                      customer.Firstname.Contains(searchTerm[Property.Lastname], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(Property.EMail) ||
                      searchTerm[Property.EMail] == "" || 
                      customer.EMail.Contains(searchTerm[Property.EMail], StringComparison.Ordinal) ? true : false; 

            result &= !searchTerm.ContainsKey(Property.Website)
                      || searchTerm[Property.Website] == "" ||
                      customer.Website.Contains(searchTerm[Property.Website], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(Property.Id) ||
                      searchTerm[Property.Street] == "" ||
                      customer.Address.Street.Contains(searchTerm[Property.Street], StringComparison.OrdinalIgnoreCase) ? true : false;
            
            result &= !searchTerm.ContainsKey(Property.StreetNumber) ||
                      searchTerm[Property.StreetNumber] == ""||
                      customer.Address.StreetNumber.Contains(searchTerm[Property.StreetNumber], StringComparison.Ordinal) ? true : false;

            result &= !searchTerm.ContainsKey(Property.Zip) ||
                      searchTerm[Property.Zip] == "" ||
                      customer.Address.Zip.Contains(searchTerm[Property.Zip], StringComparison.Ordinal) ? true : false;

            result &= !searchTerm.ContainsKey(Property.Country) ||
                      searchTerm[Property.Country] == "" ||
                      customer.Address.Country.Contains(searchTerm[Property.Country], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(Property.City) ||
                      searchTerm[Property.City] == "" ||
                      customer.Address.City.Contains(searchTerm[Property.City], StringComparison.OrdinalIgnoreCase) ? true : false;

            return result;
        }
    }
}
