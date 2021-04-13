using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Model;
using PaymentGateway.Repository;
using PaymentGateway.Utility;
using System;

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentRepository { get; set; }
        ILogger _logger;

        public PaymentController(IPaymentService paymentRepository, ILogger<PaymentController> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        /// <summary>
        /// Process Payment after validating payment request
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
                {
                    return BadRequest();
                }
                else
                {
                    var paymentResponse = _paymentRepository.ProcessPayment(paymentRequest);
                    if (paymentResponse.Status == "Success")
                        return Ok(paymentResponse);
                    else
                        return BadRequest(paymentResponse);
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.AddErrorLogs(ex, _logger, "PaymentRequest : " + JsonConvert.SerializeObject(paymentRequest));

                return StatusCode(500);
            }
        }
    }
}
