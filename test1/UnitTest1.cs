using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace test1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetTotalRevenueTest()
        {
            string fromDate = "getdate()";
            string toDate = "getdate()";
            int backNumber = -7;

            var result = DashboardService.GetTotalRevenue(fromDate, toDate, backNumber);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalChannelSaleTest()
        {
            string fromDate = "getdate()";
            string toDate = "getdate()";
            int backNumber = -7;

            var result = DashboardService.GetTotalChannelSale(fromDate, toDate, backNumber);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LoadPaymentPageTest()
        {
            var result = PaymentService.LoadPaymentPage();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotalFeedbackTest()
        {
            string fromDate = "getdate()";
            string toDate = "getdate()";
            int backNumber = -7;

            var result = DashboardService.GetTotalFeedback(fromDate, toDate, backNumber);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GrowUpLastMonthByCountryTest()
        {
            string fromDate = "getdate()";
            string toDate = "getdate()";

            var result = DashboardService.GetPercentageGrowUp(fromDate, toDate);

            Assert.IsNotNull(result);


        }
    }
}
