using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    class CustomerRepository
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
        private static string ConnectionString { get; set; }

        public CustomerRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public CustomerDto GetCustomerById(int id)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var customer = context.Customers.Find(id);

                if (customer != null)
                    return new CustomerDto()
                    {
                        Id = customer.Id,
                        Firstname = customer.Firstname,
                        Lastname = customer.Lastname,
                        EMail = customer.EMail,
                        Password = customer.Password,
                        Website = customer.Website,
                        Address = customer.Address
                    };
                else
                {
                    return null;
                }
            }
        }

        public List<CustomerDto> GetCustomersBySearchTerm(Dictionary<Property, String> searchTerm)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                List<CustomerDto> customerList = new List<CustomerDto>();

                context.Customers
                    .Where(customer => EvaluateSerachTerm(searchTerm, customer))
                    .ToList()
                    .ForEach(customer => customerList.Add(
                            new CustomerDto()
                            {
                                Id = customer.Id,
                                Firstname = customer.Firstname,
                                Lastname = customer.Lastname,
                                EMail = customer.EMail,
                                Password = customer.Password,
                                Website = customer.Website,
                                Address = customer.Address
                            }
                        )
                    );

                return customerList;
            }
        }

        public List<CustomerDto> GetAllCustomers()
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                List<CustomerDto> customerList = new List<CustomerDto>();
                    
                context.Customers.ToList()
                    .ForEach(customer => customerList.Add(
                            new CustomerDto()
                            {
                                Id = customer.Id,
                                Firstname = customer.Firstname,
                                Lastname = customer.Lastname,
                                EMail = customer.EMail,
                                Password = customer.Password,
                                Website = customer.Website,
                                Address = customer.Address
                            }
                        )
                    );

                return customerList;
            }
        }

        public void AddNewCustomer(CustomerDto customerDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Customers.Add(customerDto);
                context.SaveChanges();
            }
        }

        public void DeleteCustomerByDto(CustomerDto customerDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Customers.Remove(customerDto);
                context.SaveChanges();
            }
        }

        public void UpdateCustomerByDto(CustomerDto customerDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                context.Customers.Update(customerDto);
                context.SaveChanges();
            }
        }

        public void SetAddressByCustomerDtoAndAddressDto(CustomerDto customerDto, AddressDto addressDto)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                var customer = context.Customers.Find(customerDto);
                var address = context.Addresses.Find(addressDto);

                if (customer == null)
                    return;

                if (address != null)
                    customer.Address = address;
                else
                {
                    AddressRepository addressRepository = new AddressRepository(ConnectionString);
                    addressRepository.AddNewAddress(addressDto);
                    address = context.Addresses.Find(addressDto);
                }

                customer.Address = address;
            }
        }

        private bool EvaluateSerachTerm(Dictionary<Property, String> searchTerm, Customer customer)
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
