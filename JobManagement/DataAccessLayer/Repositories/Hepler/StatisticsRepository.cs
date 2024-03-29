﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Context;
using DataAccessLayer.Interfaces.Helper;
using DataAccessLayer.QueryTypes;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Helper
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public DataTable GetStatisticData()
        {
            DataTable dataTable = new DataTable();

            AddHeaderData(dataTable);
            AddRowData(dataTable, "Anzahl Aufträge", GetNumberOfOrdersByQuarter());
            AddRowData(dataTable, "Anzahl verwaltete Artikel", GetNumberOfItemsByQuarter());
            AddRowData(dataTable, "Durchschnittliche Anzahl Artikel pro Auftrag", GetAverageNumberOfItemsInOrdersByQuarter());
            AddRowData(dataTable, GetTotalCustomerSalesByQuarter());
            AddRowData(dataTable, "Gesamtumsatz", GetTotalSalesByQuarter());

            return dataTable;
        }

        private int GetQuarterFromDate(DateTime date)
        {
            return (date.Month + 2) / 3;
        }

        private void AddHeaderData(DataTable dataTable)
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

        private void AddRowData(DataTable dataTable, string category, Dictionary<string, string> statisticData)
        {
            DataRow catRow = dataTable.NewRow();
            catRow["Kategorie"] = category;

            foreach (var item in statisticData)
            {
                catRow[item.Key] = item.Value;
            }

            dataTable.Rows.Add(catRow);
        }

        private void AddRowData(DataTable dataTable, Dictionary<string, Dictionary<string, string>> statisticCustomerData)
        {
            foreach (var dataset in statisticCustomerData)
            {
                DataRow catRow = dataTable.NewRow();
                catRow["Kategorie"] = dataset.Key;
                foreach (var item in dataset.Value)
                {
                    catRow[item.Key] = item.Value;
                }
                dataTable.Rows.Add(catRow);
            }
        }

        private Dictionary<string, string> GetNumberOfOrdersByQuarter()
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

        private Dictionary<string, string> GetNumberOfItemsByQuarter()
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

        private Dictionary<string, string> GetAverageNumberOfItemsInOrdersByQuarter()
        {
            using (var context = new JobManagementContext())
            {
                return context.AverageItemNumbersPerOrderRequest.FromSqlRaw(
                        @"
                        WITH ORDER_ITEMS AS
                            (SELECT
		                        ORDER_ID,
		                        AMOUNT,
		                        YEAR,
		                        QUARTER
	                        FROM
		                        (SELECT
			                        Positions.Amount AS 'AMOUNT', 
			                        Positions.OrderId AS 'ORDER_ID',
			                        Orders.Date AS 'DATE',
			                        YEAR(Orders.Date) AS 'YEAR',
			                        CASE
				                        WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 1 THEN 1
				                        WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 2 AND CAST(MONTH(Orders.Date) as decimal) > 1 THEN 2
				                        WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 3 AND CAST(MONTH(Orders.Date) as decimal) > 2 THEN 3
				                        ELSE 4
			                        END AS 'QUARTER'
		                            FROM [JobManagement].[dbo].[Positions]
		                            FULL JOIN Orders ON [JobManagement].[dbo].[Positions].[OrderId] = [JobManagement].[dbo].[Orders].[Id]
		                        WHERE PeriodStart >= DATEADD(year, -3, GETDATE())
		                        ) innerquery
	                        GROUP BY ORDER_ID, AMOUNT, YEAR, QUARTER),

                        ITEMS_QUARTER AS
	                        (SELECT*,
		                        CONCAT(YEAR, ' ', 'Q', QUARTER) AS CREATION_DATE,
		                        CAST(AVG(AMOUNT) OVER(PARTITION BY YEAR, QUARTER ORDER BY YEAR, QUARTER) AS nvarchar)
	                        AS 'TOTAL_AVERAGE_QUARTERLY'
	                        FROM ORDER_ITEMS)

                        SELECT DISTINCT CREATION_DATE, TOTAL_AVERAGE_QUARTERLY
                        FROM ITEMS_QUARTER
                            ORDER BY CREATION_DATE     
                        "
                    )
                    .ToDictionary(res => res.CREATION_DATE, res => res.TOTAL_AVERAGE_QUARTERLY);
            }
        }

        private Dictionary<string, string> GetTotalSalesByQuarter()
        {
            using (var context = new JobManagementContext())
            {
                return context.TotalSalesRequest.FromSqlRaw(
                        @"
                        WITH TOTAL_SALES AS
                            (SELECT
		                        POSITION_TOTAL,
		                        YEAR,
		                        QUARTER
	                        FROM
		                        (SELECT
			                        ((Items.Price + (Items.Price / 100 * Items.Vat)) * Positions.Amount) AS 'POSITION_TOTAL',
			                        YEAR(Orders.Date) AS 'YEAR',
			                        CASE
				                        WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 1 THEN 1
				                        WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 2 AND CAST(MONTH(Orders.Date) as decimal) > 1 THEN 2
				                        WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 3 AND CAST(MONTH(Orders.Date) as decimal) > 2 THEN 3
				                        ELSE 4
			                        END AS 'QUARTER'
		                            FROM [JobManagement].[dbo].[Positions]
			                        FULL JOIN Items ON [JobManagement].[dbo].[Positions].ItemId = [JobManagement].[dbo].[Items].[Id]
		                            FULL JOIN Orders ON [JobManagement].[dbo].[Positions].[OrderId] = [JobManagement].[dbo].[Orders].[Id]
		                        WHERE Orders.PeriodStart >= DATEADD(year, -3, GETDATE())
		                        ) innerquery),

                        SALES_QUARTER AS
	                        (SELECT*,
		                        CONCAT(YEAR, ' ', 'Q', QUARTER) AS CREATION_DATE,
		                        CAST(SUM(POSITION_TOTAL) OVER(PARTITION BY YEAR, QUARTER ORDER BY YEAR, QUARTER) AS nvarchar)
	                        AS 'TOTAL_SALES_QUARTERLY'
	                        FROM TOTAL_SALES)

                        SELECT DISTINCT CREATION_DATE, TOTAL_SALES_QUARTERLY
                        FROM SALES_QUARTER
                            ORDER BY CREATION_DATE   
                        "
                    )
                    .ToDictionary(res => res.CREATION_DATE, res => res.TOTAL_SALES_QUARTERLY);
            }
        }

        private Dictionary<string, Dictionary<string, string>> GetTotalCustomerSalesByQuarter()
        {
            using (var context = new JobManagementContext())
            {
                return ConvertCustomerDataListToDictionary(context.TotalCustomersSalesRequest.FromSqlRaw(
                        @"
                            WITH CUSTOMER_SALES AS
                                (SELECT
		                            CUSTOMER_NAME,
		                            POSITION_TOTAL,
		                            YEAR,
		                            QUARTER
	                            FROM
		                            (SELECT
			                            CONCAT(Customers.Firstname, ' ', Customers.Lastname) AS 'CUSTOMER_NAME',
			                            ((Items.Price + (Items.Price / 100 * Items.Vat)) * Positions.Amount) AS 'POSITION_TOTAL',
			                            YEAR(Orders.Date) AS 'YEAR',
			                            CASE
				                            WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 1 THEN 1
				                            WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 2 AND CAST(MONTH(Orders.Date) as decimal) > 1 THEN 2
				                            WHEN CAST(MONTH(Orders.Date) as decimal) / 3 <= 3 AND CAST(MONTH(Orders.Date) as decimal) > 2 THEN 3
				                            ELSE 4
			                            END AS 'QUARTER'
		                            FROM [JobManagement].[dbo].[Positions]
			                            FULL JOIN Items ON [JobManagement].[dbo].[Positions].ItemId = [JobManagement].[dbo].[Items].[Id]
		                                FULL JOIN Orders ON [JobManagement].[dbo].[Positions].[OrderId] = [JobManagement].[dbo].[Orders].[Id]
			                            FULL JOIN Customers ON [JobManagement].[dbo].[Orders].[CustomerId] = [JobManagement].[dbo].[Customers].[Id]
		                            WHERE Orders.PeriodStart >= DATEADD(year, -3, GETDATE())
		                            ) innerquery),

                            CUSTOMER_SALES_QUARTER AS
	                            (SELECT*,
		                            CONCAT(YEAR, ' ', 'Q', QUARTER) AS CREATION_DATE,
		                            CAST(SUM(POSITION_TOTAL) OVER(PARTITION BY CUSTOMER_NAME, YEAR, QUARTER ORDER BY YEAR, QUARTER) AS nvarchar)
	                            AS 'TOTAL_SALES_QUARTERLY'
	                            FROM CUSTOMER_SALES)

                            SELECT DISTINCT CUSTOMER_NAME, CREATION_DATE, TOTAL_SALES_QUARTERLY
                            FROM CUSTOMER_SALES_QUARTER
                                ORDER BY CREATION_DATE     
                        "
                    )
                    .ToList());
            }
        }

        private Dictionary<string, Dictionary<string, string>> ConvertCustomerDataListToDictionary(List<TotalCustomersSalesRequest> customerDataList)
        {
            Dictionary<string, Dictionary<string, string>> customerDataDict = new Dictionary<string, Dictionary<string, string>>();

            foreach (var dataset in customerDataList)
            {
                if (!customerDataDict.ContainsKey(dataset.CUSTOMER_NAME))
                    customerDataDict.Add(dataset.CUSTOMER_NAME, new Dictionary<string, string>());
                customerDataDict[dataset.CUSTOMER_NAME].Add(dataset.CREATION_DATE, dataset.TOTAL_SALES_QUARTERLY);
            }

            return customerDataDict;
        }
    }
}
