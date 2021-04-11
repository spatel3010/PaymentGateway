using Microsoft.Extensions.Configuration;
using PaymentGateway.Model;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PaymentGateway
{
    public class PremiumPaymentGateway : IPaymentGateway
    {
        public int RetryCount { get; set; }
        RazorpayClient razorpayClient;
        IConfiguration _configuration;

        public PremiumPaymentGateway(IConfiguration configuration)
        {
            RetryCount = 3;
            _configuration = configuration;
            var clientId = _configuration["ClientId"];
            var clientSecret = _configuration["ClientSecret"];
            razorpayClient = new RazorpayClient(clientId, clientSecret);
        }

        public bool IsAvailable()
        {
            return true;
        }

        public bool MakePayment(PaymentRequest paymentRequest)
        {
            bool isPaymentSuccess = false;
            try
            {
                while (RetryCount > 0 && !isPaymentSuccess)
                {
                    //Do payment
                    var options = new Dictionary<string, object>();

                    options.Add("amount", 10);

                    options.Add("currency", "INR");

                    Payment payment = razorpayClient.Payment.Capture(options);

                    isPaymentSuccess = true;
                }

            }
            catch (Exception)
            {
                //Add error logs
                Thread.Sleep(500);
                RetryCount = RetryCount - 1;
                
                isPaymentSuccess = false;
            }
            return isPaymentSuccess;
        }
    }
}
