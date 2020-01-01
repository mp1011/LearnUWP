using FinancialDucks.Models;

namespace FinancialDucks.Services.Validations.Field
{
    public class GreaterThanZeroValidator<TModel> : FieldValidator<TModel, decimal>
    {
        public override string PropertyName { get; }

        public GreaterThanZeroValidator(string propertyName)
        {
            PropertyName = propertyName;
        }
        protected override ValidationResult Validate(decimal fieldValue)
        {
            if (fieldValue <= 0)
                return ValidationResult.Invalid(PropertyName, $"{PropertyName} must be greater than $0");
            else
                return ValidationResult.OK(PropertyName);
        }
    }
}
