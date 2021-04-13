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

        /// <summary>
        /// Is Expensive payment gateway is available or not
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable()
        {
            return true;
        }

        /// <summary>
        /// Make payment using Expensive payment gateway
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
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
