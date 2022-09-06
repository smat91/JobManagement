using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Helper
{
    internal interface IStatisticsConnection
    {
        DataTable GetStatisticData();
    }
}
