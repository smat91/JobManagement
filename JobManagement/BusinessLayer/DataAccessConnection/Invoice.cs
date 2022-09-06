using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces.Helper;

namespace BusinessLayer.DataAccessConnection
{
    public class Invoice : IInvoiceConnection
    {
        private readonly IInvoiceRepository invoiceRepository_;

        public Invoice(IInvoiceRepository invoiceRepository)
        {
            invoiceRepository_ = invoiceRepository;
        }

        public List<InvoiceDto> GetInvoicesByFilterTerm(string filterTerm)
        {
            var invoiceRequests = invoiceRepository_.GetInvoicesByFilterTerm(filterTerm);
            return InvoiceDto.InvoiceListToInvoiceDtoList(invoiceRequests);
        }
    }
}
