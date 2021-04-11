using PaymentGateway.Model;
using System;
using System.Threading;

namespace PaymentGateway
{
    public class PremiumPaymentGateway : IPaymentGateway
    {
        public int RetryCount { get; set; }

        public PremiumPaymentGateway()
        {
            //Register PremiumPaymentGateway
            RetryCount = 3;
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
