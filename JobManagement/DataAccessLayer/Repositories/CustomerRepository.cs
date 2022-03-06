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
        public static ICustomer GetCustomerById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var customer = context.Customers.Find(id);
                return customer;
            }
        }

        public static List<ICustomer> GetCustomersBySearchTerm(Dictionary<ICustomerProperties.Property, string> searchTerm)
        {
            using (var context = new JobManagementContext())
            {
                List<ICustomer> customerList = new List<ICustomer>();

                context.Customers
                    .Where(customer => EvaluateSearchTerm(searchTerm, customer))
                    .ToList()
                    .ForEach(customer => customerList.Add(customer)
                    );

                return customerList;
            }
        }

        public static List<ICustomer> GetAllCustomers()
        {
            using (var context = new JobManagementContext())
            {
                List<ICustomer> customerList = new List<ICustomer>();
                    
                context.Customers.ToList()
                    .ForEach(customer => customerList.Add(customer));

                return customerList;
            }
        }

        public static void AddNewCustomer(ICustomer customer)
        {
            using (var context = new JobManagementContext())
            {
                if (customer.Address != null)
                {
                    var address = context.Addresses
                        .Find(customer.Address.Id);
                    if (address != null)
                        customer.Address = address;
                }

                context.Customers.Add((Customer)customer);
                context.SaveChanges();
            }
        }

        public static void DeleteCustomerByDto(ICustomer customer)
        {
            using (var context = new JobManagementContext())
            {
                context.Customers.Remove((Customer)customer);
                context.SaveChanges();
            }
        }

        public static void UpdateCustomerByDto(ICustomer customer)
        {
            using (var context = new JobManagementContext())
            {
                if (customer.Address != null)
                {
                    var address = context.Addresses
                        .Find(customer.Address.Id);
                    if (address != null)
                        customer.Address = address;
                }

                context.Customers.Update((Customer)customer);
                context.SaveChanges();
            }
        }

        public static void SetAddressByCustomerDtoAndAddressDto(ICustomer customer, IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                var customerTemp = context.Customers.Find(customer.Id);
                var addressTemp = context.Addresses.Find(address.Id);

                if (customerTemp == null)
                    return;

                if (addressTemp != null)
                    customerTemp.Address = addressTemp;
                else
                {
                    addressTemp = (Address)address;
                }

                customerTemp.Address = addressTemp;
            }
        }

        private static bool EvaluateSearchTerm(Dictionary<ICustomerProperties.Property, string> searchTerm, ICustomer customer)
        {
            bool result = true;

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Id) || 
                      searchTerm[ICustomerProperties.Property.Id] == "" || 
                      customer.Id.ToString().Contains(searchTerm[ICustomerProperties.Property.Id], StringComparison.OrdinalIgnoreCase);

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Firstname) ||
                      searchTerm[ICustomerProperties.Property.Firstname] == "" ||
                      customer.Firstname.Contains(searchTerm[ICustomerProperties.Property.Firstname], StringComparison.OrdinalIgnoreCase);

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Lastname) ||
                      searchTerm[ICustomerProperties.Property.Lastname] == "" ||
                      customer.Firstname.Contains(searchTerm[ICustomerProperties.Property.Lastname], StringComparison.OrdinalIgnoreCase);

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.EMail) ||
                      searchTerm[ICustomerProperties.Property.EMail] == "" || 
                      customer.EMail.Contains(searchTerm[ICustomerProperties.Property.EMail], StringComparison.Ordinal); 

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Website)
                      || searchTerm[ICustomerProperties.Property.Website] == "" ||
                      customer.Website.Contains(searchTerm[ICustomerProperties.Property.Website], StringComparison.OrdinalIgnoreCase);

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Id) ||
                      searchTerm[ICustomerProperties.Property.Street] == "" ||
                      customer.Address.Street.Contains(searchTerm[ICustomerProperties.Property.Street], StringComparison.OrdinalIgnoreCase);
            
            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.StreetNumber) ||
                      searchTerm[ICustomerProperties.Property.StreetNumber] == ""||
                      customer.Address.StreetNumber.Contains(searchTerm[ICustomerProperties.Property.StreetNumber], StringComparison.Ordinal);

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Zip) ||
                      searchTerm[ICustomerProperties.Property.Zip] == "" ||
                      customer.Address.Zip.Contains(searchTerm[ICustomerProperties.Property.Zip], StringComparison.Ordinal);

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.Country) ||
                      searchTerm[ICustomerProperties.Property.Country] == "" ||
                      customer.Address.Country.Contains(searchTerm[ICustomerProperties.Property.Country], StringComparison.OrdinalIgnoreCase);

            result &= !searchTerm.ContainsKey(ICustomerProperties.Property.City) ||
                      searchTerm[ICustomerProperties.Property.City] == "" ||
                      customer.Address.City.Contains(searchTerm[ICustomerProperties.Property.City], StringComparison.OrdinalIgnoreCase);

            return result;
        }
    }
}
