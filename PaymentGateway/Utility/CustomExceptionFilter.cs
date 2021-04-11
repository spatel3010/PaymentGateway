using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentGateway.Utility
{
    public class CustomExceptionFilter :  ExceptionFilterAttribute
    {
        ILoggerFactory loggerFactory = new LoggerFactory();
        private ILogger _logger;
        public override void OnException(ExceptionContext exceptionContext)
        {
            _logger = new Logger<CustomExceptionFilter>(loggerFactory);

            string exceptionMessage = string.Empty;
            if (exceptionContext.Exception.InnerException == null)
            {
                exceptionMessage = exceptionContext.Exception.Message;
            }
            else
            {
                exceptionMessage = exceptionContext.Exception.InnerException.Message;
            }
            //We can log this exception message to the file or database.  
            _logger.LogError(exceptionMessage, exceptionContext.RouteData);

            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("An unhandled exception was thrown by service."),  
                    ReasonPhrase = "Internal Server Error.Please Contact your Administrator."
            };
            //exceptionContext.Result = IActionResult response;
        }
    }
}
