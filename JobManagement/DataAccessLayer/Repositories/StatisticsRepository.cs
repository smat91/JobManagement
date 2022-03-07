﻿using System;
using System.Collections.Generic;
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
        public static List<ExpandoObject> GetStatisticData()
        {
            List<ExpandoObject> objList = new List<ExpandoObject>();

			var headers = GenerateHeaderData();

            objList.Add(AddStatisticData("Anzahl Aufträge", headers, GetNumberOfOrdersByQuarter()));
            //objList.Add(AddStatisticData("Anzahl verwaltete Artikel", headers, GetNumberOfItemsByQuarter()));

			return objList;
        }
		
        private static ExpandoObject AddStatisticData(string category, List<string> header, Dictionary<string, decimal> statisticDataRaw)
        {
            ExpandoObject obj = new ExpandoObject();

            foreach (var column in header)
            {
                if (column == "Kategorie")
                {
                    AddProperty(obj, "Kategorie", category);
                }
                else if (statisticDataRaw.ContainsKey(column))
                {
                    AddProperty(obj, column, statisticDataRaw[column]);
                }
                else
                {
                    AddProperty(obj, column, null);
                }
            }

            return obj;
        }

        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            var expandoDict = expando as IDictionary<String, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }

		private static int GetQuarterFromDate(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        private static List<string> GenerateHeaderData()
        {
            List<string> headers = new List<string>();

			// add header data
			headers.Add("Kategorie");

            for (int i = 0; i < 12; i++)
            {
                int quarter = GetQuarterFromDate(DateTime.Today.AddMonths(-i * 3));
                string header = DateTime.Today.AddMonths(-i * 3).Year + " Q" + quarter;
                headers.Add(header);
            }

			return headers;
        }

		private static Dictionary<string, decimal> GetNumberOfOrdersByQuarter()
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
		                        COUNT(Id)
		                        OVER(PARTITION BY YEAR, QUARTER  ORDER BY YEAR, QUARTER)
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

        private static Dictionary<string, decimal> GetNumberOfItemsByQuarter()
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

                    SELECT DISTINCT CREATION_DATE, TOTAL_QUARTERLY
                    INTO #TEMP
                    FROM ITEMS_QUARTER
                        ORDER BY CREATION_DATE         
                    "
					)
					.ToDictionary(res => res.CREATION_DATE, res => res.TOTAL_QUARTERLY);
			}
		}
    }
}
