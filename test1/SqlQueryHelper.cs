using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    internal class SqlQueryHelper
    {
        private static string _query = "default";

        internal static string GetTotalFeedbackQuery(string fromDate, string toDate, int backNumber)
        {
            return _query = string.Format($@"SELECT count(fdk.Id) AS NumberOfFeedback,
                                                    datename(MONTH, fdk.CreatedAt) AS MonthChar,
                                                    month(fdk.CreatedAt) AS MonthCol
                                            FROM Feedback AS fdk
                                            WHERE 
                                            fdk.CreatedAt >= DATEADD(day, (DATEDIFF(day, 0, {fromDate}) +{backNumber}), 0)
                                            and fdk.CreatedAt < DATEADD(day, (DATEDIFF(day, 0, {toDate}) +1), 0)
                                            GROUP BY datename(MONTH, fdk.CreatedAt),
                                                        month(fdk.CreatedAt);");
        }

        internal static string GetFeedbackQuery(string fromDate, string toDate, int backNumber)
        {
            return _query = string.Format($@"SELECT count(fdk.Id) AS NumberOfFeedback,
                                                   datename(MONTH, fdk.CreatedAt) AS MonthChar,
                                                   month(fdk.CreatedAt) AS MonthCol
                                            FROM Feedback AS fdk
                                            WHERE 
                                            fdk.CreatedAt >= DATEADD(day, (DATEDIFF(day, 0, {fromDate}) + {backNumber}), 0)
                                            and fdk.CreatedAt < DATEADD(day, (DATEDIFF(day, 0, {toDate}) +1), 0)
                                            GROUP BY datename(MONTH, fdk.CreatedAt),
                                                     month(fdk.CreatedAt);");
        }

        internal static string GetPaymentListQuery()
        {
            return _query = string.Format(@"DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT DECLARE @MaxTablePage AS FLOAT
                                                    SET @PageNumber=1
                                                    SET @RowsOfPage=10
                                                    SELECT @MaxTablePage = COUNT (*)
                                                    FROM Payment
                                                    SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage) 
                                                    WHILE @MaxTablePage >= @PageNumber BEGIN
                                                    SELECT pmt.PaymentMethod,
                                                           pmt.CardNumber,
                                                           pmt.CreatedAt,
                                                           pmt.CreatedAt AS LastUpdated
                                                    FROM Payment AS pmt
                                                    ORDER BY pmt.PaymentMethod
                                                    OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
                                                    SET @PageNumber = @PageNumber + 1 END
                                                    ");
        }

        internal static string GetPaymentPageQuery()
        {
            return _query = string.Format(@"DECLARE @PageNumber AS INT DECLARE @RowsOfPage AS INT DECLARE @MaxTablePage AS FLOAT
                                                    SET @PageNumber=1
                                                    SET @RowsOfPage=10
                                                    SELECT @MaxTablePage = COUNT (*)
                                                    FROM Payment
                                                    SET @MaxTablePage = CEILING(@MaxTablePage/@RowsOfPage) 
                                                    WHILE @MaxTablePage >= @PageNumber BEGIN
                                                    SELECT pmt.PaymentMethod,
                                                           pmt.CardNumber,
                                                           pmt.CreatedAt,
                                                           pmt.CreatedAt AS LastUpdated
                                                    FROM Payment AS pmt
                                                    ORDER BY pmt.PaymentMethod
                                                    OFFSET (@PageNumber-1)*@RowsOfPage ROWS FETCH NEXT @RowsOfPage ROWS ONLY
                                                    SET @PageNumber = @PageNumber + 1 END
                                                    ");
        }

        internal static string GetTotalRevenueByDayWithDapperQuery(string fromDate, string toDate, int backNumber)
        {
            return _query = string.Format($@"SELECT SUM(odi.TotalPrice) AS RevenueCol,
   	                                            DATENAME(Day, odi.OrderedAt) AS DayCol,
   	                                               Datename(month, odi.OrderedAt) as MonthCol     
                                            FROM OrderInfo AS odi
                                            WHERE odi.OrderStatus NOT IN ('RETURNED', 'FAILED', 'CANCELLED')
                                              AND odi.OrderedAt >= DATEADD(DAY, (DATEDIFF(DAY, 0, {fromDate}) + {backNumber}), 0)
                                              AND odi.OrderedAt < DATEADD(DAY, (DATEDIFF(DAY, 0, {toDate}) + 1), 0)
                                            GROUP BY DATENAME(DAY, odi.OrderedAt),
     	                                            Datename(month, odi.OrderedAt)");
        }

        internal static string GetTotalRevenueByQueryV2(string fromDate, string toDate, int backNumber)
        {
            return _query = string.Format($@"SELECT SUM(odi.TotalPrice) AS RevenueCol,
   	                                            DATENAME(Day, odi.OrderedAt) AS DayCol,
   	                                               Datename(month, odi.OrderedAt) as MonthCol     
                                            FROM OrderInfo AS odi
                                            WHERE odi.OrderStatus NOT IN ('RETURNED', 'FAILED', 'CANCELLED')
                                              AND odi.OrderedAt >= DATEADD(DAY, (DATEDIFF(DAY, 0, {fromDate}) + {backNumber}), 0)
                                              AND odi.OrderedAt < DATEADD(DAY, (DATEDIFF(DAY, 0, {toDate}) + 1), 0)
                                            GROUP BY DATENAME(DAY, odi.OrderedAt),
     	                                            Datename(month, odi.OrderedAt)");
        }

        internal static string GetTotalChannelSaleQuery(string fromDate, string toDate, int backNumber)
        {
            return _query = ($@"
                                WITH tmpChannelMonth AS
                                    (SELECT MonthValue,
                                            c.*
                                    FROM (SELECT (1) AS MonthValue
                                        UNION SELECT 2
                                        UNION SELECT 3
                                        UNION SELECT 4
                                        UNION SELECT 5
                                        UNION SELECT 6
                                        UNION SELECT 7
                                        UNION SELECT 8
                                        UNION SELECT 9
                                        UNION SELECT 10
                                        UNION SELECT 11
                                        UNION SELECT 12) t
                                    CROSS JOIN Channel AS c)
                                SELECT chn.ChannelName,
                                        Sum(odi.TotalPrice) as Totals,
                                        Datename(Month, odi.OrderedAt) AS MonthCol,
	                                    Datename(Day, odi.OrderedAt) AS MonthCol
                                FROM tmpChannelMonth chn
                                LEFT JOIN OrderInfo AS odi ON odi.ChannelId = chn.Id
                                AND chn.MonthValue = Month(odi.OrderedAt)
                                WHERE odi.OrderStatus NOT IN ('RETURNED', 'FAILED', 'CANCELLED')
                                    AND OrderedAt >= DATEADD(Day, (DATEDIFF(Day, 0, {fromDate}) +{backNumber}), 0)
                                    and OrderedAt < DATEADD(Day, (DATEDIFF(Day, 0, {toDate}) +1), 0)
                                GROUP BY chn.ChannelName,
                                            Datename(Month, odi.OrderedAt),
		                                    Datename(Day, odi.OrderedAt)
                                ORDER BY Datename(Day, odi.OrderedAt)");
        }



        internal static string GetPercentageGrowUpQuery(string fromDate, string toDate)
        {
            return _query = string.Format($@"SELECT Country,
                                                   Round(100*(sum(CASE
                                                                      WHEN month(OrderedAt) = month({fromDate})
                                                                           AND year(OrderedAt) = year({fromDate}) THEN TotalPrice
                                                                      ELSE 0
                                                                  END) - sum(CASE
                                                                                 WHEN month(OrderedAt) = month(dateadd(MONTH, -1, {toDate}))
                                                                                      AND year(OrderedAt) = year(dateadd(MONTH, -1, {toDate})) THEN TotalPrice
                                                                                 ELSE 0
                                                                             END)) / sum(CASE
                                                                                             WHEN month(OrderedAt) = month(dateadd(MONTH, -1, {toDate}))
                                                                                                  AND year(OrderedAt) = year(dateadd(MONTH, -1, {toDate})) THEN TotalPrice
                                                                                             ELSE 0
                                                                                         END), 0) AS PercentageGrowUp
                                            FROM OrderInfo
                                            GROUP BY Country");
        }

        internal static string GetTotaRevenueQuery(string fromDate, string toDate, int backNumber)
        {
            return _query = string.Format($@"SELECT SUM(odi.TotalPrice) AS RevenueCol,
                                           DATENAME(MONTH, odi.OrderedAt) AS MonthCharCol,
                                           YEAR(odi.OrderedAt) AS YearCol
                                    FROM OrderInfo AS odi
                                    WHERE odi.OrderStatus NOT IN ('RETURNED', 'FAILED', 'CANCELLED')
                                      AND OrderedAt >= DATEADD(DAY, (DATEDIFF(DAY, 0, {fromDate}) +{backNumber}), 0)
                                      AND Orderedat < DATEADD(DAY, (DATEDIFF(DAY, 0, {toDate}) + 1), 0)
                                    GROUP BY DATENAME(MONTH, odi.OrderedAt),
                                             YEAR(odi.OrderedAt),
                                             MONTH(odi.OrderedAt)
                                    ORDER BY YEAR(odi.OrderedAt),
                                             MONTH(odi.OrderedAt) ASC;");
        }
    }
}