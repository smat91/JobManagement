using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.HeplerRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public override string TableName => "Customer";

        public Customer GetSingleById(string customerNumber)
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

        public List<Customer> GetBySearchTerm(string searchTerm)
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

        public List<Customer> GetAll()
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

        public void Add(Customer customer)
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

        public void Update(Customer customer)
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
