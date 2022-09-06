using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Repositories.HeplerRepositories;

namespace PresentationLayer.MVVM.ViewModel
{
    public class HomeViewModel
    {
        public DataTable StatisticDataTable { get; set; }


        public HomeViewModel()
        {
            StatisticsConnection statistics = new StatisticsConnection(new StatisticsRepository());
            StatisticDataTable = statistics.GetStatisticData();
        }
    }
}
