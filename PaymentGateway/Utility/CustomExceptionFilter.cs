using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;

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

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var exceptionType = exceptionContext.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                exceptionMessage = "Access to the Web API is not authorized.";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(DivideByZeroException))
            {
                exceptionMessage = "Internal Server Error.";
                status = HttpStatusCode.InternalServerError;
            }
            else
            {
                exceptionMessage = "Not found.";
                status = HttpStatusCode.NotFound;
            }
            //exceptionContext.Result = new Json()
            //{
            //    Content = new StringContent(exceptionMessage, System.Text.Encoding.UTF8, "text/plain"),
            //    StatusCode = status
            //};

            base.OnException(exceptionContext);
        }
    }
}
