using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository
    {
        public ICustomer GetCustomerById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var customer = context.Customers.Find(id);
                context.Entry(customer).Reference(c => c.Address).Load();
                return customer;
            }
        }

        public List<ICustomer> GetCustomersBySearchTerm(string searchTerm)
        {
            using (var context = new JobManagementContext())
            {
                List<ICustomer> customerList = new List<ICustomer>();
                Search search = new Search();

                context.Customers
                    .Include(c => c.Address)
                    .AsEnumerable()
                    .Where(customer => search.EvaluateSearchTerm(searchTerm, customer))
                    .ToList()
                    .ForEach(customer => customerList.Add(customer));

                return customerList;
            }
        }

        public List<ICustomer> GetAllCustomers()
        {
            using (var context = new JobManagementContext())
            {
                List<ICustomer> customerList = new List<ICustomer>();
                    
                context.Customers
                    .Include(c => c.Address)
                    .ToList()
                    .ForEach(customer => customerList.Add(customer));

                return customerList;
            }
        }

        public void AddNewCustomer(ICustomer customer)
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

        public void DeleteCustomerByDto(ICustomer customer)
        {
            using (var context = new JobManagementContext())
            {
                context.Customers.Remove((Customer)customer);
                context.SaveChanges();
            }
        }

        public void UpdateCustomerByDto(ICustomer customer)
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

        public void SetAddressByCustomerAndAddress(ICustomer customer, IAddress address)
        {
            using (var context = new JobManagementContext())
            {
                var customerTemp = context.Customers.Find(customer.Id);
                context.Entry(customer).Reference(c => c.Address).Load();

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
    }
}
