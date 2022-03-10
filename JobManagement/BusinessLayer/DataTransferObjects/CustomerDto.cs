using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Website { get; set; }
        public AddressDto Address { get; set; }

        public CustomerDto()
        {
        }

        public CustomerDto(Customer customer)
        {
            Id = customer.Id;
            Firstname = customer.Firstname;
            Lastname = customer.Lastname;
            EMail = customer.EMail;
            Password = customer.Password;
            Website = customer.Website;
            Address = new AddressDto(customer.Address);
        }

        public static DataAccessLayer.Models.Customer CustomerDtoToCustomer(CustomerDto customer)
        {
            return new DataAccessLayer.Models.Customer
            {
                Id = customer.Id,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                EMail = customer.EMail,
                Password = customer.Password,
                Website = customer.Website,
                Address = AddressDto.AddressDtoToAddress(customer.Address)
            };
        }

        public static List<CustomerDto> CustomerListToCustomerDtoList(List<Customer> customers)
        {
            List<CustomerDto> customerDtos = new List<CustomerDto>();
            foreach (var customer in customers)
            {
                customerDtos.Add(new CustomerDto(customer));
            }

            return customerDtos;
        }
    }
}
