using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;

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
