using DataAccessLayer.QueryTypes;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLayer.Repositories.InvoiceRepository;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class InvoiceConnection
    {
        private readonly InvoiceRepository invoiceRepository_;

        public InvoiceConnection(string connectionString)
        {
            invoiceRepository_ = new InvoiceRepository(connectionString);
        }

        public List<InvoiceRequest> GetInvoicesByFilterTerm(Dictionary<Property, String> filterTerm)
        {
            var invoices = invoiceRepository_.GetInvoicesByFilterTerm(filterTerm);
            return invoices;
        }
    }
}
