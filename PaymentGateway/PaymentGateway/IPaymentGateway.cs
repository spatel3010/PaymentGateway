using PaymentGateway.Model;

namespace PaymentGateway
{
    public interface IPaymentGateway
    {
        int RetryCount { get; set; }
        bool IsAvailable();
        PaymentResponse MakePayment(PaymentRequest paymentRequest);
    }
}
