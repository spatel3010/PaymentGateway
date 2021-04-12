using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PaymentGateway.Model;
using System;

namespace PaymentGateway.Utility
{
    public class ExceptionHelper
    {
        public static void AddErrorLogs(Exception exception, ILogger logger, string message = "")
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail();

            if (!string.IsNullOrEmpty(message))
                exceptionDetail.Message = message;

            exceptionDetail.Source = exception.Source;
            exceptionDetail.StackTrace = exception.StackTrace;

            if (exception.InnerException != null)
                exceptionDetail.InnerException = exception.InnerException.ToString();

            if (exception.HelpLink != null)
                exceptionDetail.HelpLink = exception.HelpLink;

            var requestClass = exception.TargetSite != null ? exception.TargetSite.ReflectedType.FullName : null;
            var requestMethod = exception.TargetSite != null ? exception.TargetSite.Name : null;
            var timeUtc = DateTime.Now;

            exceptionDetail.RequestClass = requestClass;
            exceptionDetail.RequestMethod = requestMethod;
            exceptionDetail.TimeUtc = timeUtc.ToString();

            logger.LogError("API Exception Logs :" + JsonConvert.SerializeObject(exceptionDetail));
        }
    }
}
