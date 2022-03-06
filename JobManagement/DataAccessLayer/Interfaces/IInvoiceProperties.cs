using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IInvoiceProperties
    { 
        public enum Property
        {
            CustomerId,
            Name,
            Date,
            DateOlderThan,
            DateNewerThan,
            Street,
            Country,
            City
        }
    }
}
