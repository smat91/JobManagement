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
			                        Id,
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
	                        GROUP BY Id, YEAR, QUARTER),

                        SALES_QUARTER AS
	                        (SELECT*,
		                        CONCAT(YEAR, ' ', 'Q', QUARTER) AS ORDER_DATE,
		                        COUNT(Id)
		                        OVER(PARTITION BY YEAR, QUARTER  ORDER BY YEAR, QUARTER)
	                        AS 'TOTAL_QUARTERLY'
	                        FROM SALES)

                        SELECT DISTINCT ORDER_DATE, TOTAL_QUARTERLY, 'SALES' AS 'CATEGORY'
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

	                        SET @sql = 'SELECT CATEGORY, ' + @columns + ' FROM 
					                        (
						                        SELECT DISTINCT
							                        CATEGORY,
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

        private Dictionary<string, decimal> GetNumberOfItemsByQuarter()
		{
			using (var context = new JobManagementContext(ConnectionString))
			{
				return context.OrderNumbersRequest.FromSqlRaw(
					@"
                    WITH ITEMS AS
                        (SELECT
		                    Id,
		                    YEAR,
		                    QUARTER
	                    FROM
		                    (SELECT
			                    Production.Product.ProductID AS 'Id',
			                    SellStartDate,
			                    YEAR(SellStartDate) AS 'YEAR',
			                    CASE
				                    WHEN CAST(MONTH(SellStartDate) as decimal) / 3 <= 1 THEN 1
				                    WHEN CAST(MONTH(SellStartDate) as decimal) / 3 <= 2 AND CAST(MONTH(SellStartDate) as decimal) > 1 THEN 2
				                    WHEN CAST(MONTH(SellStartDate) as decimal) / 3 <= 3 AND CAST(MONTH(SellStartDate) as decimal) > 2 THEN 3
				                    ELSE 4
			                    END AS 'QUARTER'
		                    FROM Production.Product
		                    WHERE SellStartDate >= DATEADD(year, -20, GETDATE())
		                    ) innerquery
	                    GROUP BY Id, YEAR, QUARTER),

                    ITEMS_QUARTER AS
	                    (SELECT*,
		                    CONCAT(YEAR, ' ', 'Q', QUARTER) AS CREATION_DATE,
		                    COUNT(Id)
		                    OVER(PARTITION BY YEAR, QUARTER  ORDER BY YEAR, QUARTER)
	                    AS 'TOTAL_QUARTERLY'
	                    FROM ITEMS)

                    SELECT DISTINCT CREATION_DATE, TOTAL_QUARTERLY, 'ITEMS' AS 'CATEGORY'
                    INTO #TEMP
                    FROM ITEMS_QUARTER
                        ORDER BY CREATION_DATE

                    IF (SELECT COUNT(*) FROM #TEMP) > 0
	                    DECLARE 
		                    @columns NVARCHAR(MAX) = '', 
		                    @sql     NVARCHAR(MAX) = '';

	                    -- select the category names
	                    SELECT 
		                    @columns+=QUOTENAME(CREATION_DATE) + ','
	                    FROM 
		                    #TEMP
	                    ORDER BY 
		                    CREATION_DATE

	                    -- remove the last comma
	                    SET @columns = LEFT(@columns, LEN(@columns) - 1);

	                    SET @sql = 'SELECT CATEGORY, ' + @columns + ' FROM 
					                    (
						                    SELECT DISTINCT
							                    CATEGORY,
							                    TOTAL_QUARTERLY,
							                    CREATION_DATE
						                    FROM #TEMP
					                    ) sel
					                    PIVOT 
					                    (
						                    max(TOTAL_QUARTERLY)
						                    for CREATION_DATE in (' + @columns + ')
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
