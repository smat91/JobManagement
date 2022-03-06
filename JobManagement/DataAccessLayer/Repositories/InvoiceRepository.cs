using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class InvoiceRepository
    {
        public enum Property
        {
            CustomerId,
            Name,
            Date,
            DateOlderThan,
            DateNewerThan,
            Street,
            Country,
            City
        }

        public Dictionary<Property, String> FilterTerm { get; set; }

        private static string ConnectionString { get; set; }

        public InvoiceRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<InvoiceRequest> GetInvoicesByFilterTerm(Dictionary<Property, String> filterTerm)
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                List <InvoiceRequest> invoices = new List<InvoiceRequest>();

                var orders = context.Orders
                    .Where(order => EvaluateFilterTerm(filterTerm, order));

                if (orders != null)
                    foreach (var order in orders)
                    {
                        invoices.Add(OrderToInvoice(context, order));
                    }
                
                return invoices;
            }
        }

        private InvoiceRequest OrderToInvoice(JobManagementContext context, Order order)
        {
            var customer = context.Customers
                .TemporalAsOf(order.Date)
                .Single(customer => customer.Id == order.Customer.Id);

            var priceNet = context.Positions
                .TemporalAsOf(order.Date)
                .Where(o => o.Id == order.Id)
                .Sum(p => p.Amount * p.Item.Price);

            var priceGross = context.Positions
                .TemporalAsOf(order.Date)
                .Where(o => o.Id == order.Id)
                .Sum(p => p.Amount * (p.Item.Price / 100 * (100 + p.Item.Vat)));

            return new InvoiceRequest()
            {
                CustomerId = order.Customer.Id,
                Name = customer.Firstname + " " + customer.Lastname,
                Street = customer.Address.Street + " " + customer.Address.StreetNumber,
                Zip = customer.Address.Zip,
                City = customer.Address.City,
                Country = customer.Address.Country,
                Date = order.Date,
                InvoiceNumber = order.Id,
                PriceNet = priceNet,
                PriceGross = priceGross
            };
        }

        private bool EvaluateFilterTerm(Dictionary<Property, String> filterTerm, Order order)
        {
            bool result = true;

            result &= !filterTerm.ContainsKey(Property.CustomerId) ||
                      filterTerm[Property.CustomerId] == "" ||
                      order.Customer.Id.ToString().Contains(filterTerm[Property.CustomerId], StringComparison.OrdinalIgnoreCase);

            result &= !filterTerm.ContainsKey(Property.Name) ||
                      filterTerm[Property.Name] == "" ||
                      (order.Customer.Firstname + " " + order.Customer.Lastname)
                      .Contains(filterTerm[Property.Name], StringComparison.OrdinalIgnoreCase);

            result &= !filterTerm.ContainsKey(Property.Date) ||
                      filterTerm[Property.Date] == "" ||
                      (order.Date == Convert.ToDateTime(filterTerm[Property.Date]));

            result &= !filterTerm.ContainsKey(Property.Date) ||
                      filterTerm[Property.Date] == "" ||
                      (order.Date < Convert.ToDateTime(filterTerm[Property.DateOlderThan]));

            result &= !filterTerm.ContainsKey(Property.Date) ||
                      filterTerm[Property.Date] == "" ||
                      (order.Date > Convert.ToDateTime(filterTerm[Property.DateNewerThan]));

            result &= !filterTerm.ContainsKey(Property.Street) ||
                      filterTerm[Property.Street] == "" ||
                      (order.Customer.Address.Street + " " + order.Customer.Address.StreetNumber)
                      .Contains(filterTerm[Property.Street], StringComparison.Ordinal);

            result &= !filterTerm.ContainsKey(Property.Country)
                      || filterTerm[Property.Country] == "" ||
                      order.Customer.Address.Country.Contains(filterTerm[Property.Country], StringComparison.OrdinalIgnoreCase);

            result &= !filterTerm.ContainsKey(Property.City) ||
                      filterTerm[Property.Street] == "" ||
                      order.Customer.Address.City.Contains(filterTerm[Property.Street], StringComparison.OrdinalIgnoreCase);

            return result;
        }
    }
}
