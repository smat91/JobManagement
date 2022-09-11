using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Castle.Core.Internal;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Helper;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public override string TableName => "Customer";

        public Customer GetSingleById(int id)
        {
            using (var context = new JobManagementContext())
            {
                var customer = context.Customers
                    .Include(customer => customer.Address)
                    .Single(customer => customer.Id == id);
                context.Entry(customer).Reference(c => c.Address).Load();
                return customer;
            }
        }

        public new List<Customer> GetBySearchTerm(string searchTerm)
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

        public new List<Customer> GetAll()
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

        public new List<Customer> GetAll(DateTime date)
        {
            using (var context = new JobManagementContext())
            {
                List<Customer> customerList = new List<Customer>();

                context.Customers
                    .TemporalAsOf(date)
                    .Include(customer => customer.Address)
                    .ToList()
                    .ForEach(customer => customerList.Add(customer));

                return customerList;
            }
        }

        public new void Add(Customer customer)
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

        public new void Update(Customer customer)
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

        public void ImportCustomers(string filePath, string fileType)
        {
            List<Customer> customers = new List<Customer>();

            if (fileType == "json")
            {
                customers = JsonToCutomerList(File.ReadAllText(filePath));
            }

            foreach (var customer in customers) {
                var customerSearch = GetBySearchTerm(customer.CustomerNumber).FirstOrDefault();

                if (customerSearch == null)
                {
                    Add(customer);
                }
                else {
                    customer.Id = customerSearch.Id;
                    customer.Address.Country = customerSearch.Address.Country;
                    customer.Address.City = customerSearch.Address.City;
                    Update(customer);
                }
            }
        }

        public void ExportCustomers(string filePath, string fileType, DateTime date)
        {
            var customerList = GetAll(date);


            if (fileType == "json")
            {
                File.WriteAllText(filePath,CutomerListToJson(customerList));
            }

        }

        private List<Customer> JsonToCutomerList(string reader)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new CustomerToJsonConverter() }
            };

            return JsonSerializer.Deserialize<List<Customer>>(reader, options);
        }

        private string CutomerListToJson(List<Customer> customerList) 
        {
            var options = new JsonSerializerOptions { 
                WriteIndented = true,
                Converters = {new CustomerToJsonConverter() }
            };

            return JsonSerializer.Serialize(customerList, options);
        }
    }
}
