using PaymentGateway.Model;
using System;

namespace PaymentGateway.Repository
{
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// Make payment by chosing appropriate payment gateway
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public bool ProcessPayment(PaymentRequest paymentRequest)
        {
            IPaymentGateway paymentGateway;
            try
            {
                if (paymentRequest.Amount <= 20)
                {
                    paymentGateway = new CheapPaymentGateway();
                }
                else if (paymentRequest.Amount > 20 && paymentRequest.Amount <= 500)
                {
                    paymentGateway = new ExpensivePaymentGateway();
                    if (!paymentGateway.IsAvailable())
                        paymentGateway = new CheapPaymentGateway();
                }
                else
                {
                    paymentGateway = new PremiumPaymentGateway();
                }
                return paymentGateway.MakePayment(paymentRequest);
            }
            catch (Exception ex)
            {
                //Add error logs
                return false;
            }
        }
    }
}
