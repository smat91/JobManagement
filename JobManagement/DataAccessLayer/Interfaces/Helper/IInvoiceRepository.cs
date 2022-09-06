using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.QueryTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Helper
{
    public interface IInvoiceRepository
    {
        List<InvoiceRequest> GetInvoicesByFilterTerm(string searchTerm);
        InvoiceRequest OrderToInvoice(JobManagementContext context, Order order);
    }
}
