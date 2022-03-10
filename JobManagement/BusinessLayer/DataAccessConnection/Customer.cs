using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
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
            return new CustomerDto(customer);
        }

        public List<CustomerDto> GetCustomersBySearchTerm(string searchTerm)
        {
            var customers = customerRepository_.GetCustomersBySearchTerm(searchTerm);
            return CustomerDto.CustomerListToCustomerDtoList(customers);
        }

        public List<CustomerDto> GetAllCustomers()
        {
            var customers = customerRepository_.GetAllCustomers();
            return CustomerDto.CustomerListToCustomerDtoList(customers);
        }

        public void AddNewCustomer(CustomerDto customer)
        {
            customerRepository_.AddNewCustomer(CustomerDto.CustomerDtoToCustomer(customer));
        }

        public void DeleteCustomerByDto(CustomerDto customer)
        {
            customerRepository_.DeleteCustomerByDto(CustomerDto.CustomerDtoToCustomer(customer));
        }

        public void UpdateCustomerByDto(CustomerDto customer)
        {
            customerRepository_.UpdateCustomerByDto(CustomerDto.CustomerDtoToCustomer(customer));
        }

        public void SetAddressByICustomerAndAddressDto (CustomerDto customer, AddressDto address)
        {
            customerRepository_.SetAddressByCustomerAndAddress(
                CustomerDto.CustomerDtoToCustomer(customer),
                AddressDto.AddressDtoToAddress(address));
        }
    }
    
}
