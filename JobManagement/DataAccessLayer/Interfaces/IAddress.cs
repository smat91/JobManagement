using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAddress
    { public int Id { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
