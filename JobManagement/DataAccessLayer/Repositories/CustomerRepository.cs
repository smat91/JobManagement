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


        public Dictionary<ICustomerProperties.Property, String> SearchTerm { get; set; }
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

        public List<ICustomer> GetCustomersBySearchTerm(Dictionary<ICustomerProperties.Property, String> searchTerm)
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

        private bool EvaluateSerachTerm(Dictionary<ICustomerProperties.Property, String> searchTerm, ICustomer customer)
        {
            bool result = true;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Id) || 
                      searchTerm[ICustomerProperties.Property.Id] == "" || 
                      customer.Id.ToString().Contains(searchTerm[ICustomerProperties.Property.Id], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Firstname) ||
                      searchTerm[ICustomerProperties.Property.Firstname] == "" ||
                      customer.Firstname.Contains(searchTerm[ICustomerProperties.Property.Firstname], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Lastname) ||
                      searchTerm[ICustomerProperties.Property.Lastname] == "" ||
                      customer.Firstname.Contains(searchTerm[ICustomerProperties.Property.Lastname], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.EMail) ||
                      searchTerm[ICustomerProperties.Property.EMail] == "" || 
                      customer.EMail.Contains(searchTerm[ICustomerProperties.Property.EMail], StringComparison.Ordinal) ? true : false; 

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Website)
                      || searchTerm[ICustomerProperties.Property.Website] == "" ||
                      customer.Website.Contains(searchTerm[ICustomerProperties.Property.Website], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Id) ||
                      searchTerm[ICustomerProperties.Property.Street] == "" ||
                      customer.Address.Street.Contains(searchTerm[ICustomerProperties.Property.Street], StringComparison.OrdinalIgnoreCase) ? true : false;
            
            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.StreetNumber) ||
                      searchTerm[ICustomerProperties.Property.StreetNumber] == ""||
                      customer.Address.StreetNumber.Contains(searchTerm[ICustomerProperties.Property.StreetNumber], StringComparison.Ordinal) ? true : false;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Zip) ||
                      searchTerm[ICustomerProperties.Property.Zip] == "" ||
                      customer.Address.Zip.Contains(searchTerm[ICustomerProperties.Property.Zip], StringComparison.Ordinal) ? true : false;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Country) ||
                      searchTerm[ICustomerProperties.Property.Country] == "" ||
                      customer.Address.Country.Contains(searchTerm[ICustomerProperties.Property.Country], StringComparison.OrdinalIgnoreCase) ? true : false;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.City) ||
                      searchTerm[ICustomerProperties.Property.City] == "" ||
                      customer.Address.City.Contains(searchTerm[ICustomerProperties.Property.City], StringComparison.OrdinalIgnoreCase) ? true : false;

            return result;
        }
    }
}
