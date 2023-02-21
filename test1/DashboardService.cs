using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    public static class DashboardService
    {
        private static readonly string _connectionString = "Data Source=.;Initial Catalog=OMSDb;Integrated Security=True";

        internal static IEnumerable<TotalSaleItem> GetTotalChannelSale(string fromDate, string toDate, int backNumber)
        {
            string sql = SqlQueryHelper.GetTotalChannelSaleQuery(fromDate, toDate, backNumber);
            return GetTotalSaleItem(sql);
        }

        private static IEnumerable<TotalSaleItem> GetTotalSaleItem(string sql)
        {
            return SqlHelper.Query<TotalSaleItem>(sql);
        }

        internal static IEnumerable<FeedbackItem> GetTotalFeedback(string fromDate, string toDate, int backNumber)
        {
            string sql = SqlQueryHelper.GetTotalFeedbackQuery(fromDate, toDate, backNumber);
            return GetFeedbackItems(sql);
        }

        private static IEnumerable<FeedbackItem> GetFeedbackItems(string sql)
        {
            return SqlHelper.Query<FeedbackItem>(sql);
        }
        internal static IEnumerable<RevenueItem> GetTotalRevenue(string fromDate, string toDate, int backNumber)
        {
            string sql = SqlQueryHelper.GetTotaRevenueQuery(fromDate, toDate, backNumber);
            return GetRevenueItems(sql);
        }
        private static IEnumerable<RevenueItem> GetRevenueItems(string sql)
        {
            return SqlHelper.Query<RevenueItem>(sql);
        }

        internal static IEnumerable<GrowUpItem> GetPercentageGrowUp(string fromDate, string toDate)
        {
            string sql = SqlQueryHelper.GetPercentageGrowUpQuery(fromDate, toDate);
            return GetGrowUpItem(sql);
        }

        private static IEnumerable<GrowUpItem> GetGrowUpItem(string sql)
        {
            return SqlHelper.Query<GrowUpItem>(sql);
        }
    }




}