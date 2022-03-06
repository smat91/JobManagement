using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using PresentationLayer.MVVM.ViewModel.Models;
using System;
using System.Collections.Generic;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class CustomerConnection
    {
        private readonly CustomerRepository customerRepository_;

        public CustomerConnection(string connectionString)
        {
            customerRepository_ = new CustomerRepository(connectionString);
        }

        public ICustomer GetCustomerById(int id)
        {
            var customer = customerRepository_.GetCustomerById(id);
            return customer;
        }

        public List<ICustomer> GetCustomersBySearchTerm(Dictionary<ICustomerProperties.Property, string> searchTerm)
        {
            var customersList = customerRepository_.GetCustomersBySearchTerm(searchTerm);
            return customersList;
        }

        public List<ICustomer> GetAllCustomers()
        {
            var customersList = customerRepository_.GetAllCustomers();
            return customersList;
        }

        public void AddNewCustomer(ICustomer customer)
        {
            customerRepository_.AddNewCustomer(customer);
        }

        public void DeleteCustomerByDto(ICustomer customer)
        {
            customerRepository_.DeleteCustomerByDto(customer);
        }

        public void UpdateCustomerByDto(ICustomer customer)
        {
           customerRepository_.UpdateCustomerByDto(customer);
        }

        public void SetAddressByCustomerDtoAndAddressDto (ICustomer customer, IAddress address)
        {
            customerRepository_.SetAddressByCustomerDtoAndAddressDto(customer, address);
        }

    }
    
}
