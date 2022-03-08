using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.QueryTypes
{
    public class TotalCustomersSalesRequest
    {
        public string CUSTOMER_NAME { get; set; }
        public string CREATION_DATE { get; set; }
        public string TOTAL_SALES_QUARTERLY { get; set; }
    }
}
