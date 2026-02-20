using System.ComponentModel.DataAnnotations;

namespace GameTournamentAPI.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime < DateTime.Now)
                {
                    return new ValidationResult("Date cannot be in the past");
                }
            }
            return ValidationResult.Success;
        }
    }
}
