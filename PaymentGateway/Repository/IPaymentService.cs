using PaymentGateway.Model;

namespace PaymentGateway.Repository
{
    public interface IPaymentService
    {
        PaymentResponse ProcessPayment(PaymentRequest paymentRequest);
    }
}
