using PaymentGateway.Model;
using System;

namespace PaymentGateway
{
    public class ExpensivePaymentGateway : IPaymentGateway
    {
        public int RetryCount { get; set; }
        public ExpensivePaymentGateway()
        {
            //Register ExpensivePaymentGateway
            RetryCount = 0;
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
