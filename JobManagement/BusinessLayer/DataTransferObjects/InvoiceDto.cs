﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DataTransferObjects
{
    public class InvoiceDto
    {
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal PriceNet { get; set; }
        public decimal PriceGross { get; set; }

        public InvoiceDto()
        {
        }

        public InvoiceDto(DataAccessLayer.QueryTypes.InvoiceRequest invoice)
        {
            CustomerNumber = invoice.CustomerNumber;
            Name = invoice.Street;
            Street = invoice.Street;
            Zip = invoice.Zip;
            Country = invoice.Country;
            City = invoice.City;
            Date = invoice.Date;
            InvoiceNumber = invoice.InvoiceNumber;
            PriceNet = invoice.PriceNet;
            PriceGross = invoice.PriceGross;
        }

        public static List<InvoiceDto> InvoiceListToInvoiceDtoList(List<DataAccessLayer.QueryTypes.InvoiceRequest> invoices)
        {
            List<InvoiceDto> invoiceDtos = new List<InvoiceDto>();
            foreach (var invoice in invoices)
            {
                invoiceDtos.Add(new InvoiceDto(invoice));
            }

            return invoiceDtos;
        }
    }
}
