namespace PaymentGateway.Model
{
    public class ExceptionDetail
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public string RequestClass { get; set; }
        public string RequestMethod { get; set; }
        public string TimeUtc { get; set; }
        public string HelpLink { get; set; }
    }
}
