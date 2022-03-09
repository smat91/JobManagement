using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;

namespace PresentationLayer.MVVM.ViewModel
{
    public class HomeViewModel
    {
        public DataTable StatisticDataTable { get; set; }

        public HomeViewModel()
        {
            Statistics statistics = new Statistics();
            StatisticDataTable = statistics.GetStatisticData();
        }
    }
}
