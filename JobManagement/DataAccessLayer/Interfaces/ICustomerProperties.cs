using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICustomerProperties
    { 
        public enum Property
        {
            Id,
            Firstname,
            Lastname,
            EMail,
            Website,
            Street,
            StreetNumber,
            Zip,
            Country,
            City,
        }
    }
}
