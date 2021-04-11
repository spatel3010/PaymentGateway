using Microsoft.Extensions.Configuration;
using PaymentGateway.Model;
using Razorpay.Api;
using System;
using System.Collections.Generic;

namespace PaymentGateway
{
    public class ExpensivePaymentGateway : IPaymentGateway
    {
        public int RetryCount { get; set; }
        RazorpayClient razorpayClient;
        IConfiguration _configuration;

        public ExpensivePaymentGateway(IConfiguration configuration)
        {
            //Register ExpensivePaymentGateway
            RetryCount = 0;
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
            try
            {
                //Do payment
                var options = new Dictionary<string, object>();

                options.Add("amount", paymentRequest.Amount);

                options.Add("currency", "INR");

                Payment payment = razorpayClient.Payment.Capture(options);


                return true;
            }
            catch (Exception)
            {
                //Add error logs
                return false;
            }
        }
    }
}
