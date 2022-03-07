using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Repositories;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class StatisticsConnection
    {
        public static List<ExpandoObject> GetStatisticData()
        {
            var statistics = StatisticsRepository.GetStatisticData();
            return statistics;
        }
    }
}
