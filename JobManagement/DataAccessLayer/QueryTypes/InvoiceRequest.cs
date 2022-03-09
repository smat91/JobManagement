using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.QueryTypes
{
    public class InvoiceRequest : IInvoice
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceNumber { get; set; }
        [Column(TypeName = "decimal(7,2)")]
        public decimal PriceNet { get; set; }
        [Column(TypeName = "decimal(7,2)")]
        public decimal PriceGross { get; set; }
    }
}
