using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataAccessConnection;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Repositories.Helper;

namespace PresentationLayer.MVVM.ViewModel
{
    public class HomeViewModel
    {
        public DataTable StatisticDataTable { get; set; }

        protected StatisticsConnection statisticsConnection_;
        public HomeViewModel(StatisticsConnection statisticsConnection)
        {
            this.statisticsConnection_ = statisticsConnection;
            StatisticDataTable = statisticsConnection_.GetStatisticData();
        }
    }
}
