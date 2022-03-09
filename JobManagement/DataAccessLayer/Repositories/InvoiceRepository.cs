using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class InvoiceRepository
    {
        public List<IInvoice> GetInvoicesByFilterTerm(Dictionary<IInvoiceProperties.Property, string> filterTerm)
        {
            using (var context = new JobManagementContext())
            {
                List <IInvoice> invoices = new List<IInvoice>();

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

        public IInvoice OrderToInvoice(JobManagementContext context, IOrder order)
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

        public bool EvaluateFilterTerm(Dictionary<IInvoiceProperties.Property, string> filterTerm, IOrder order)
        {
            bool result = true;

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.CustomerId) ||
                      filterTerm[IInvoiceProperties.Property.CustomerId] == "" ||
                      order.Customer.Id.ToString().Contains(filterTerm[IInvoiceProperties.Property.CustomerId], StringComparison.OrdinalIgnoreCase);

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.Name) ||
                      filterTerm[IInvoiceProperties.Property.Name] == "" ||
                      (order.Customer.Firstname + " " + order.Customer.Lastname)
                      .Contains(filterTerm[IInvoiceProperties.Property.Name], StringComparison.OrdinalIgnoreCase);

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.Date) ||
                      filterTerm[IInvoiceProperties.Property.Date] == "" ||
                      (order.Date == Convert.ToDateTime(filterTerm[IInvoiceProperties.Property.Date]));

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.Date) ||
                      filterTerm[IInvoiceProperties.Property.Date] == "" ||
                      (order.Date < Convert.ToDateTime(filterTerm[IInvoiceProperties.Property.DateOlderThan]));

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.Date) ||
                      filterTerm[IInvoiceProperties.Property.Date] == "" ||
                      (order.Date > Convert.ToDateTime(filterTerm[IInvoiceProperties.Property.DateNewerThan]));

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.Street) ||
                      filterTerm[IInvoiceProperties.Property.Street] == "" ||
                      (order.Customer.Address.Street + " " + order.Customer.Address.StreetNumber)
                      .Contains(filterTerm[IInvoiceProperties.Property.Street], StringComparison.Ordinal);

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.Country)
                      || filterTerm[IInvoiceProperties.Property.Country] == "" ||
                      order.Customer.Address.Country.Contains(filterTerm[IInvoiceProperties.Property.Country], StringComparison.OrdinalIgnoreCase);

            result &= !filterTerm.ContainsKey(IInvoiceProperties.Property.City) ||
                      filterTerm[IInvoiceProperties.Property.Street] == "" ||
                      order.Customer.Address.City.Contains(filterTerm[IInvoiceProperties.Property.Street], StringComparison.OrdinalIgnoreCase);

            return result;
        }
    }
}
