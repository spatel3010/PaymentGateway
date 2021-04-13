using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Model;
using PaymentGateway.Utility;
using System;

namespace PaymentGateway.Repository
{
    public class PaymentService : IPaymentService
    {
        IConfiguration _configuration;
        ILogger _logger;
        public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Make payment by choosing appropriate payment gateway
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public PaymentResponse ProcessPayment(PaymentRequest paymentRequest)
        {
            IPaymentGateway paymentGateway;
            PaymentResponse paymentResponse;
            try
            {
                if (paymentRequest.Amount <= 20)
                {
                    paymentGateway = new CheapPaymentGateway(_configuration, _logger);
                }
                else if (paymentRequest.Amount > 20 && paymentRequest.Amount <= 500)
                {
                    paymentGateway = new ExpensivePaymentGateway();
                    if (!paymentGateway.IsAvailable())
                        paymentGateway = new CheapPaymentGateway(_configuration, _logger);
                }
                else
                {
                    paymentGateway = new PremiumPaymentGateway();
                }

                do
                {
                    paymentResponse = paymentGateway.MakePayment(paymentRequest);
                    paymentGateway.RetryCount--;
                }
                while (paymentGateway.RetryCount > 0 && !(paymentResponse.Status == "Success"));

                return paymentResponse;
            }
            catch (Exception ex)
            {
                ExceptionHelper.AddErrorLogs(ex, _logger, "PaymentRequest : " + JsonConvert.SerializeObject(paymentRequest));
                throw;
            }
        }
    }
}
