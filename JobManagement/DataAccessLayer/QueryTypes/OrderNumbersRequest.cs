using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.QueryTypes
{
    public class OrderNumbersRequest
    {
        public string HeaderName { get; set; }
        public decimal Value { get; set; }
    }
}
