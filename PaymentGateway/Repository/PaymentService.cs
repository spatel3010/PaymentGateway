using Microsoft.Extensions.Configuration;
using PaymentGateway.Model;
using System;

namespace PaymentGateway.Repository
{
    public class PaymentService : IPaymentService
    {
        IConfiguration _configuration;
        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
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
                    paymentGateway = new CheapPaymentGateway(_configuration);
                }
                else if (paymentRequest.Amount > 20 && paymentRequest.Amount <= 500)
                {
                    paymentGateway = new ExpensivePaymentGateway(_configuration);
                    if (!paymentGateway.IsAvailable())
                        paymentGateway = new CheapPaymentGateway(_configuration);
                }
                else
                {
                    paymentGateway = new PremiumPaymentGateway(_configuration);
                }
                return paymentGateway.MakePayment(paymentRequest);
            }
            catch (Exception ex)
            {
                //Add error logs
                throw;
            }
        }
    }
}
