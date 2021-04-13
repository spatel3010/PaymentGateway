using PaymentGateway.Model;

namespace PaymentGateway
{
    public class PremiumPaymentGateway : IPaymentGateway
    {
        public int RetryCount { get; set; }

        public PremiumPaymentGateway()
        {
            RetryCount = 3;
        }

        /// <summary>
        /// IS Premium payment gateway available or not
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable()
        {
            return true;
        }

        /// <summary>
        /// Make payment using premium payment gateway
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public PaymentResponse MakePayment(PaymentRequest paymentRequest)
        {
            throw new System.Exception("Payment Failed");
        }
    }
}
