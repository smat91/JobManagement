using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IInvoice
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal PriceNet { get; set; }
        public decimal PriceGross { get; set; }
    }
}
