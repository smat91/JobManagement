using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BusinessLayer.DataAccessConnection
{
    public class Customer
    {
        private readonly CustomerRepository customerRepository_;

        public Customer()
        {
            customerRepository_ = new CustomerRepository();
        }
        public CustomerDto GetCustomerById(int id)
        {
            var customer = customerRepository_.GetCustomerById(id);
            return (CustomerDto)customer;
        }

        public List<CustomerDto> GetCustomersBySearchTerm(Dictionary<ICustomerProperties.Property, string> searchTerm)
        {
            var customersList = customerRepository_.GetCustomersBySearchTerm(searchTerm);
            return customersList.ConvertAll(
                new Converter<ICustomer, CustomerDto>(ICustomerToCustomerDto));
        }

        public List<CustomerDto> GetAllCustomers()
        {
            var customersList = customerRepository_.GetAllCustomers();
            return customersList.ConvertAll(
                new Converter<ICustomer, CustomerDto>(ICustomerToCustomerDto));
        }

        public void AddNewCustomer(CustomerDto customer)
        {
            customerRepository_.AddNewCustomer(customer);
        }

        public void DeleteCustomerByDto(CustomerDto customer)
        {
            customerRepository_.DeleteCustomerByDto(customer);
        }

        public void UpdateCustomerByDto(CustomerDto customer)
        {
            customerRepository_.UpdateCustomerByDto(customer);
        }

        public void SetAddressByCustomerDtoAndAddressDto (CustomerDto customer, AddressDto address)
        {
            customerRepository_.SetAddressByCustomerDtoAndAddressDto(customer, address);
        }

        private static CustomerDto ICustomerToCustomerDto(ICustomer customer)
        {
            return (CustomerDto)customer;
        }

    }
    
}
