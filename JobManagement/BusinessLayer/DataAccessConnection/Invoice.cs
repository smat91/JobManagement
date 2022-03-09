using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using DataAccessLayer.QueryTypes;
using DataAccessLayer.Repositories;

namespace BusinessLayer.DataAccessConnection
{
    public class Invoice
    {
        private readonly InvoiceRepository invoiceRepository_;

        public Invoice()
        {
            invoiceRepository_ = new InvoiceRepository();
        }

        public List<InvoiceDto> GetInvoicesByFilterTerm(Dictionary<IInvoiceProperties.Property, string> filterTerm)
        {
            var invoices = invoiceRepository_.GetInvoicesByFilterTerm(filterTerm);
            return InvoiceDto.InvoiceListToInvoiceDtoList(invoices);
        }
    }
}
