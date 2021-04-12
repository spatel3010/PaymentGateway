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

        public bool IsAvailable()
        {
            return true;
        }

        public PaymentResponse MakePayment(PaymentRequest paymentRequest)
        {
            throw new System.Exception("Payment Failed");
        }
    }
}
