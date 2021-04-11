using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentGateway.Model
{
    public class PaymentRequest
    {
        public string TransactionId { get; set; }
        [Required(ErrorMessage ="Credit card is required.")]
        [CreditCard]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Card holder name is required.")]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "Expiration date is required.")]
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public Decimal Amount { get; set; }
    }
}
