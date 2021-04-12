using PaymentGateway.Model;

namespace PaymentGateway
{
    public class ExpensivePaymentGateway : IPaymentGateway
    {
        public int RetryCount { get; set; }

        public ExpensivePaymentGateway()
        {
            //Register ExpensivePaymentGateway
            RetryCount = 1;
        }

        public bool IsAvailable()
        {
            return true;
        }

        public PaymentResponse MakePayment(PaymentRequest paymentRequest)
        {
            var paymentResponse = new PaymentResponse
            {
                Amount = paymentRequest.Amount,
                Status = "Success",
                TransactionId = paymentRequest.TransactionId
            };
            return paymentResponse;
        }
    }
}
