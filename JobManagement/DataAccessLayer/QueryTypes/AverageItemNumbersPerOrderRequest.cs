using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.QueryTypes
{
    public class AverageItemNumbersPerOrderRequest
    {
        public string CREATION_DATE { get; set; }
        public string TOTAL_AVERAGE_QUARTERLY { get; set; }
    }
}
