using System;

namespace test1
{
    internal class PaymentItem
    {
        public string PaymentMethod { get;  set; }
        public string CardNumber { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public DateTime LastUpdated { get;  set; }
    }
}