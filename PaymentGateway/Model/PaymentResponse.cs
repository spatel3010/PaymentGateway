using System;

namespace PaymentGateway.Model
{
    public class PaymentResponse
    {
        public string TransactionId { get; set; }
        public int Status { get; set; }
        public Decimal Amount { get; set; }
        public string Message { get; set; }
    }
}
