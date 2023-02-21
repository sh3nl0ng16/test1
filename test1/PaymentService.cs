using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace test1
{
    internal class PaymentService
    {
        private static readonly string _connectionString = "Data Source=.;Initial Catalog=OMSDb;Integrated Security=True";
        internal static IEnumerable<PaymentItem> LoadPaymentPage()
        {
            string sql = SqlQueryHelper.GetPaymentPageQuery();
            return GetPaymentItems(sql);
        }

        private static IEnumerable<PaymentItem> GetPaymentItems(string sql)
        {
            return SqlHelper.Query<PaymentItem>(sql);
        }
    }
}