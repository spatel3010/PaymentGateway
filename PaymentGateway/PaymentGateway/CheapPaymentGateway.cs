using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Model;
using PaymentGateway.Repository;
using PaymentGateway.Utility;
using Razorpay.Api;
using System;
using System.Collections.Generic;

namespace PaymentGateway
{
    public class CheapPaymentGateway : IPaymentGateway
    {
        public int RetryCount { get; set; }
        RazorpayClient razorpayClient;
        IConfiguration _configuration;
        ILogger _logger;

        public CheapPaymentGateway(IConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            RetryCount = 1;
            _configuration = configuration;
            var clientId = _configuration["ClientId"];
            var clientSecret = _configuration["ClientSecret"];
            razorpayClient = new RazorpayClient(clientId, clientSecret);
        }

        /// <summary>
        /// Is Cheap payment gateway available or not
        /// </summary>
        /// <returns></returns>
        public bool IsAvailable()
        {
            return true;
        }

        /// <summary>
        /// Make payment using Cheap payment gateway
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public PaymentResponse MakePayment(PaymentRequest paymentRequest)
        {
            bool isPaymentSuccess = false;

            try
            {
                //Do payment
                var options = new Dictionary<string, object>();

                options.Add("amount", paymentRequest.Amount);

                options.Add("currency", "INR");

                Payment payment = razorpayClient.Payment.Capture(options);

                isPaymentSuccess = payment.Attributes["status"] == "captured";

                var paymentResponse = new PaymentResponse
                {
                    Amount = paymentRequest.Amount,
                    Status = isPaymentSuccess ? "Success" : "Failed",
                    TransactionId = paymentRequest.TransactionId
                };

                return paymentResponse;
            }
            catch (Exception ex)
            {
                ExceptionHelper.AddErrorLogs(ex, _logger, "PaymentRequest : " + JsonConvert.SerializeObject(paymentRequest));

                return new PaymentResponse
                {
                    Amount = paymentRequest.Amount,
                    Status = "Failed",
                    Message = ex.Message,
                    TransactionId = paymentRequest.TransactionId
                };
            }
        }

    }
}
