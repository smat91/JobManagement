using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class CustomerConnection
    {
        public static ICustomer GetCustomerById(int id)
        {
            var customer = CustomerRepository.GetCustomerById(id);
            return customer;
        }

        public static List<ICustomer> GetCustomersBySearchTerm(Dictionary<ICustomerProperties.Property, string> searchTerm)
        {
            var customersList = CustomerRepository.GetCustomersBySearchTerm(searchTerm);
            return customersList;
        }

        public static List<ICustomer> GetAllCustomers()
        {
            var customersList = CustomerRepository.GetAllCustomers();
            return customersList;
        }

        public static void AddNewCustomer(ICustomer customer)
        {
            CustomerRepository.AddNewCustomer(customer);
        }

        public static void DeleteCustomerByDto(ICustomer customer)
        {
            CustomerRepository.DeleteCustomerByDto(customer);
        }

        public static void UpdateCustomerByDto(ICustomer customer)
        {
            CustomerRepository.UpdateCustomerByDto(customer);
        }

        public static void SetAddressByCustomerDtoAndAddressDto (ICustomer customer, IAddress address)
        {
            CustomerRepository.SetAddressByCustomerDtoAndAddressDto(customer, address);
        }

    }
    
}
