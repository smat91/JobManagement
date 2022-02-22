using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class StatisticsRepository
    {
        private static string ConnectionString { get; set; }

        public StatisticsRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public List<Dictionary<string, decimal>> GetStatisticData()
        {
            return null;
        }

        private Dictionary<string, decimal> GetNumberOfOrdersByQuarter()
        {
            using (var context = new JobManagementContext(ConnectionString))
            {
                return context.OrderNumbersRequest.FromSqlRaw(
                        @"
                        WITH SALES AS
                            (SELECT
		                        Id,
		                        YEAR,
		                        QUARTER
	                        FROM
		                        (SELECT
			                        SalesOrderID AS 'Id',
			                        Date,
			                        YEAR(Date) AS 'YEAR',
			                        CASE
				                        WHEN CAST(MONTH(Date) as decimal) / 3 <= 1 THEN 1
				                        WHEN CAST(MONTH(Date) as decimal) / 3 <= 2 AND CAST(MONTH(Date) as decimal) > 1 THEN 2
				                        WHEN CAST(MONTH(Date) as decimal) / 3 <= 3 AND CAST(MONTH(Date) as decimal) > 2 THEN 3
				                        ELSE 4
			                        END AS 'QUARTER'
		                        FROM[dbo].[Orders]
		                        WHERE Date >= DATEADD(year, -3, GETDATE())
		                        ) innerquery
	                        GROUP BY ORDER_ID, YEAR, QUARTER),

                        SALES_QUARTER AS
	                        (SELECT*,
		                        CONCAT(YEAR, ' ', 'Q', QUARTER) AS ORDER_DATE,
		                        COUNT(ORDER_ID)
		                        OVER(PARTITION BY YEAR, QUARTER  ORDER BY YEAR, QUARTER)
	                        AS 'TOTAL_QUARTERLY'
	                        FROM SALES)

                        SELECT DISTINCT ORDER_DATE, TOTAL_QUARTERLY, 'CATEGORY' AS 'SALES'
                        INTO #TEMP
                        FROM SALES_QUARTER
                            ORDER BY ORDER_DATE

                        IF (SELECT COUNT(*) FROM #TEMP) > 0
	                        DECLARE 
		                        @columns NVARCHAR(MAX) = '', 
		                        @sql     NVARCHAR(MAX) = '';

	                        -- select the category names
	                        SELECT 
		                        @columns+=QUOTENAME(ORDER_DATE) + ','
	                        FROM 
		                        #TEMP
	                        ORDER BY 
		                        ORDER_DATE

	                        -- remove the last comma
	                        SET @columns = LEFT(@columns, LEN(@columns) - 1);

	                        SET @sql = 'SELECT SALES, ' + @columns + ' FROM 
					                        (
						                        SELECT DISTINCT
							                        SALES,
							                        TOTAL_QUARTERLY,
							                        ORDER_DATE
						                        FROM #TEMP
					                        ) sel
					                        PIVOT 
					                        (
						                        max(TOTAL_QUARTERLY)
						                        for ORDER_DATE in (' + @columns + ')
					                        ) piv '

	                        EXECUTE(@sql)

                        DROP TABLE #TEMP;
                        "
                    )
                    .ToDictionary(res => res.HeaderName, res => res.Value);
            }
        }
    }
}
