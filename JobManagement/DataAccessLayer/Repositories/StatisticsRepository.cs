using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class StatisticsRepository
    {
        public static DataTable GetStatisticData()
        {
            DataTable dataTable = new DataTable();

            AddHeaderData(dataTable);
            AddRowData(dataTable, "Anzahl Aufträge", GetNumberOfOrdersByQuarter());
            AddRowData(dataTable, "Anzahl verwaltete Artikel", GetNumberOfItemsByQuarter());

			return dataTable;
        }
        
		private static int GetQuarterFromDate(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        private static void AddHeaderData(DataTable dataTable)
        {
			// add header data
            dataTable.Columns.Add("Kategorie");

            for (int i = 0; i < 12; i++)
            {
                int quarter = GetQuarterFromDate(DateTime.Today.AddMonths(-i * 3));
                string header = DateTime.Today.AddMonths(-i * 3).Year + " Q" + quarter;
                dataTable.Columns.Add(header);
            }
        }

        private static void AddRowData(DataTable dataTable, string category, Dictionary<string, string> statisticData)
        {
            DataRow catRow = dataTable.NewRow();
            catRow["Kategorie"] = category;

			foreach (var item in statisticData)
            {
                catRow[item.Key] = item.Value;
			}

            dataTable.Rows.Add(catRow);
		}

		private static Dictionary<string, string> GetNumberOfOrdersByQuarter()
        {
            using (var context = new JobManagementContext())
            {
                return context.OrderNumbersRequest.FromSqlRaw(
						@"
                        WITH ORDERS AS
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
		                        FROM [dbo].[Orders]
		                        WHERE Date >= DATEADD(year, -3, GETDATE())
		                        ) innerquery
	                        GROUP BY Id, YEAR, QUARTER),

                        ORDERS_QUARTER AS
	                        (SELECT*,
		                        CONCAT(YEAR, ' ', 'Q', QUARTER) AS ORDER_DATE,
		                        CAST(COUNT(Id) OVER(PARTITION BY YEAR, QUARTER  ORDER BY YEAR, QUARTER) AS nvarchar)
	                        AS 'TOTAL_QUARTERLY'
	                        FROM ORDERS)

                        SELECT DISTINCT ORDER_DATE, TOTAL_QUARTERLY
                        FROM ORDERS_QUARTER
                            ORDER BY ORDER_DATE
                        "
					)
                    .ToDictionary(res => res.ORDER_DATE, res => res.TOTAL_QUARTERLY);
            }
        }

        private static Dictionary<string, string> GetNumberOfItemsByQuarter()
		{
			using (var context = new JobManagementContext())
			{
				return context.ItemNumbersRequest.FromSqlRaw(
					@"
                    WITH ITEMS AS
                        (SELECT
		                    Id,
		                    YEAR,
		                    QUARTER
	                    FROM
		                    (SELECT
			                    Id,
			                    PeriodStart,
			                    YEAR(PeriodStart) AS 'YEAR',
			                    CASE
				                    WHEN CAST(MONTH(PeriodStart) as decimal) / 3 <= 1 THEN 1
				                    WHEN CAST(MONTH(PeriodStart) as decimal) / 3 <= 2 AND CAST(MONTH(PeriodStart) as decimal) > 1 THEN 2
				                    WHEN CAST(MONTH(PeriodStart) as decimal) / 3 <= 3 AND CAST(MONTH(PeriodStart) as decimal) > 2 THEN 3
				                    ELSE 4
			                    END AS 'QUARTER'
		                    FROM
			                    (SELECT	Id,	PeriodStart	FROM [dbo].[Items]
			                    UNION
			                    SELECT Id, PeriodStart FROM [dbo].[ItemsHistory]) innerquery
		                    WHERE PeriodStart >= DATEADD(year, -3, GETDATE())
		                    ) innerquery
	                    GROUP BY Id, YEAR, QUARTER),

                    ITEMS_QUARTER AS
	                    (SELECT*,
		                    CONCAT(YEAR, ' ', 'Q', QUARTER) AS CREATION_DATE,
		                    CAST(COUNT(Id) OVER(PARTITION BY YEAR, QUARTER  ORDER BY YEAR, QUARTER) AS nvarchar)
	                    AS 'TOTAL_QUARTERLY'
	                    FROM ITEMS)

                    SELECT DISTINCT CREATION_DATE, TOTAL_QUARTERLY
                    FROM ITEMS_QUARTER
                        ORDER BY CREATION_DATE         
                    "
					)
					.ToDictionary(res => res.CREATION_DATE, res => res.TOTAL_QUARTERLY);
			}
		}
    }
}
