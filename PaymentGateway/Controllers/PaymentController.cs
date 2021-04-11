using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Model;
using PaymentGateway.Repository;
using PaymentGateway.Utility;
using System;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
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
        [HttpPost]
        [Route("ProcessPayment")]
        public IActionResult ProcessPayment(PaymentRequest paymentRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                else
                {
                    //Process payment method call
                    var isSuccess = _paymentRepository.ProcessPayment(paymentRequest);
                    if (isSuccess)
                        return Ok();
                    else
                        return BadRequest(); //Need to return internal server error
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
