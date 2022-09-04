using System.Data;
using DataAccessLayer.Repositories.HeplerRepositories;

namespace BusinessLayer.DataAccessConnection
{
    public class Statistics
    {
        private readonly StatisticsRepository statisticsRepository_;

        public Statistics()
        {
            statisticsRepository_ = new StatisticsRepository();
        }

        public DataTable GetStatisticData()
        {
            var statistics = statisticsRepository_.GetStatisticData();
            return statistics;
        }
    }
}
