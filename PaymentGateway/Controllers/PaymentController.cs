using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Model;
using PaymentGateway.Repository;
using System;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentRepository { get; set; }
        public PaymentController(IPaymentService paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        /// <summary>
        /// Process Payment
        /// </summary>
        /// <param name="paymentRequest"></param>
        /// <returns></returns>
        public ActionResult ProcessPayment(PaymentRequest paymentRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                else
                {
                    _paymentRepository.ProcessPayment(paymentRequest);
                    //Process payment method call
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                throw;
                //Add exception to logs                 
            }
        }
    }
}
