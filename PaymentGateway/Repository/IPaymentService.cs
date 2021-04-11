using PaymentGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Repository
{
    public interface IPaymentService
    {
        bool ProcessPayment(PaymentRequest paymentRequest);
    }
}
