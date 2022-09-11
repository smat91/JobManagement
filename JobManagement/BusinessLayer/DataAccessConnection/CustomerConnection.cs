using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Microsoft.VisualBasic.FileIO;

namespace BusinessLayer.DataAccessConnection
{
    public class CustomerConnection : ICustomerConnection
    {
        private readonly ICustomerRepository customerRepository_;

        public CustomerConnection(ICustomerRepository customerRepository)
        {
            customerRepository_ = customerRepository;
        }

        public CustomerDto GetSingleById(int id)
        {
            var customer = customerRepository_.GetSingleById(id);
            return new CustomerDto(customer);
        }

        public List<CustomerDto> GetBySearchTerm(string searchTerm)
        {
            var customers = customerRepository_.GetBySearchTerm(searchTerm);
            return CustomerDto.CustomerListToCustomerDtoList(customers);
        }

        public List<CustomerDto> GetAll()
        {
            var customers = customerRepository_.GetAll();
            return CustomerDto.CustomerListToCustomerDtoList(customers);
        }

        public void Add(CustomerDto customer)
        {
            customerRepository_.Add(CustomerDto.CustomerDtoToCustomer(customer));
        }

        public string Delete(CustomerDto customer)
        {
            return customerRepository_.Delete(CustomerDto.CustomerDtoToCustomer(customer));
        }

        public void Update(CustomerDto customer)
        {
            customerRepository_.Update(CustomerDto.CustomerDtoToCustomer(customer));
        }

        public void SetAddressByCustomerDtoAndAddressDto (CustomerDto customer, AddressDto address)
        {
            customerRepository_.SetAddressByCustomerAndAddress(
                CustomerDto.CustomerDtoToCustomer(customer),
                AddressDto.AddressDtoToAddress(address));
        }

        public void ImportCustomers(string filePath, string fileType)
        {
            customerRepository_.ImportCustomers(filePath, fileType);
        }

        public void ExportCustomers(string filePath, string fileType, DateTime date)
        {
            customerRepository_.ExportCustomers(filePath, fileType, date);
        }
    }
}
