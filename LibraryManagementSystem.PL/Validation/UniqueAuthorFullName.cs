using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.PL.Validation
{
    public class UniqueAuthorFullName: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string fullName) return new ValidationResult("Full name is required.");

            var parts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4 || parts.Any(p => p.Length < 2))
                return new ValidationResult("Full name must contain four names, each at least 2 characters.");

            return ValidationResult.Success;
        }
    }
}
