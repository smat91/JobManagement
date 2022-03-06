using DataAccessLayer.QueryTypes;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using static DataAccessLayer.Repositories.InvoiceRepository;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class InvoiceConnection
    {
        public static List<InvoiceRequest> GetInvoicesByFilterTerm(Dictionary<IInvoiceProperties.Property, string> filterTerm)
        {
            var invoices = InvoiceRepository.GetInvoicesByFilterTerm(filterTerm);
            return invoices;
        }
    }
}
