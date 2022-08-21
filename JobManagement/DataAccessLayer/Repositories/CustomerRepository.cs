using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository
    {
        public Customer GetCustomerByCustomerNumber(string customerNumber)
        {
            using (var context = new JobManagementContext())
            {
                var customer = context.Customers
                    .Include(customer => customer.Address)
                    .Single(customer => customer.CustomerNumber == customerNumber);
                context.Entry(customer).Reference(c => c.Address).Load();
                return customer;
            }
        }

        public List<Customer> GetCustomersBySearchTerm(string searchTerm)
        {
            using (var context = new JobManagementContext())
            {
                List<Customer> customerList = new List<Customer>();
                Search search = new Search();

                context.Customers
                    .Include(customer => customer.Address)
                    .AsEnumerable()
                    .Where(customer => search.EvaluateSearchTerm(searchTerm, customer))
                    .ToList()
                    .ForEach(customer => customerList.Add(customer));

                return customerList;
            }
        }

        public List<Customer> GetAllCustomers()
        {
            using (var context = new JobManagementContext())
            {
                List<Customer> customerList = new List<Customer>();
                    
                context.Customers
                    .Include(customer => customer.Address)
                    .ToList()
                    .ForEach(customer => customerList.Add(customer));

                return customerList;
            }
        }

        public void AddNewCustomer(Customer customer)
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

                context.Customers.Add(customer);
                context.SaveChanges();
            }
        }

        public string DeleteCustomerByDto(Customer customer)
        {
            using (var context = new JobManagementContext())
            {
                context.Customers.Remove(customer);

                try
                {
                    context.SaveChanges();
                    return "Datensatz erfolgreich gelöscht";
                }
                catch (DbUpdateException e)
                {
                    return "Datensatz konnte nicht gelöscht werden.\nBitte zuerst Datensätze erntfernen in denen der Datensatz verwendet wird.";
                }
            }
        }

        public void UpdateCustomerByDto(Customer customer)
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

                context.Customers.Update(customer);
                context.SaveChanges();
            }
        }

        public void SetAddressByCustomerAndAddress(Customer customer, Address address)
        {
            using (var context = new JobManagementContext())
            {
                var customerTemp = context.Customers
                    .Include(c => c.Address)
                    .Single(c => c.Id == customer.Id);

                var addressTemp = context.Addresses.Find(address.Id);

                if (addressTemp != null)
                    customerTemp.Address = addressTemp;
                else
                {
                    addressTemp = address;
                }

                customerTemp.Address = addressTemp;

                context.Customers.Update(customerTemp);
                context.SaveChanges();
            }
        }
    }
}
