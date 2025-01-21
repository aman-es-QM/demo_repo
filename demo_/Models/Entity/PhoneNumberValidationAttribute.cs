using System.ComponentModel.DataAnnotations;

namespace demo_.Models.Entity
{
    public class PhoneNumberValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Phone number is required.");
            }

            string phoneNumber = value.ToString();

            // Check if the phone number has exactly 10 digits
            if (phoneNumber.Length == 10 && long.TryParse(phoneNumber, out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Phone number must be exactly 10 digits.");
        }
    }
}
