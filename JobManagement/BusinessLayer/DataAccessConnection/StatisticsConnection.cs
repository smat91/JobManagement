using System.Data;
using BusinessLayer.Interfaces.Helper;
using DataAccessLayer.Interfaces.Helper;

namespace BusinessLayer.DataAccessConnection
{
    public class StatisticsConnection : IStatisticsConnection
    {
        private readonly IStatisticsRepository statisticsRepository_;

        public StatisticsConnection(IStatisticsRepository statisticsRepository)
        {
            statisticsRepository_ = statisticsRepository;
        }

        public DataTable GetStatisticData()
        {
            var statistics = statisticsRepository_.GetStatisticData();
            return statistics;
        }
    }
}
