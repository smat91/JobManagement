using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Helper;
using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Helper
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public List<InvoiceRequest> GetInvoicesByFilterTerm(string searchTerm)
        {
            using (var context = new JobManagementContext())
            {
                List<InvoiceRequest> invoices = new List<InvoiceRequest>();
                Search search = new Search();

                var orders = context.Orders
                    .Include(order => order.Customer)
                    .ThenInclude(customer => customer.Address)
                    .Include(order => order.Positions)
                    .ThenInclude(positions => positions.Item)
                    .ThenInclude(item => item.Group)
                    .ThenInclude(group => group.ParentItemGroup)
                    .Where(order => search.EvaluateSearchTerm(searchTerm, order));

                if (orders != null)
                    foreach (var order in orders)
                    {
                        invoices.Add(OrderToInvoice(context, order));
                    }

                return invoices;
            }
        }

        public InvoiceRequest OrderToInvoice(JobManagementContext context, Order order)
        {
            var customer = context.Customers
                .TemporalAsOf(order.Date)
                .Single(customer => customer.CustomerNumber == order.Customer.CustomerNumber);

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
                CustomerNumber = order.Customer.CustomerNumber,
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
    }
}
