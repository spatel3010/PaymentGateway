using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PaymentGateway.Model
{
    public class PaymentRequest : IValidatableObject
    {
        public string TransactionId { get; set; }
        [Required(ErrorMessage = "Credit card is required.")]
        [CreditCard]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Card holder name is required.")]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "Expiration date is required.")]
        public string ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public Decimal Amount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Amount <= 0)
            {
                yield return new ValidationResult("Please Enter valid amount.", (new List<string> { "Amount" }).AsEnumerable());
            }
            if (!string.IsNullOrEmpty(SecurityCode) && SecurityCode.Length != 3)
            {
                yield return new ValidationResult("Security code must be 3 digit length.", (new List<string> { "SecurityCode" }).AsEnumerable());
            }
            var expirationDate = new DateTime();
            var result = DateTime.TryParse(ExpirationDate, out expirationDate);
            if (!result)
            {
                yield return new ValidationResult("Please enter valid expiration date.", (new List<string> { "ExpirationDate" }).AsEnumerable());
            }
            if (result && expirationDate <= DateTime.Now)
            {
                yield return new ValidationResult("Expiration date must be a future date.", (new List<string> { "ExpirationDate" }).AsEnumerable());
            }
        }
    }
}
