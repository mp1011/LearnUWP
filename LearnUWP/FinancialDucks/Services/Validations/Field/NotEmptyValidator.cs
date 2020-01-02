using FinancialDucks.Models;

namespace FinancialDucks.Services.Validations.Field
{
    public class NotEmptyValidator<TModel> : FieldValidator<TModel,string>
    {
        public override string PropertyName { get; }

        public NotEmptyValidator(string propertyName)
        {
            PropertyName = propertyName;
        }

        protected override ValidationResult Validate(string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue) || string.IsNullOrWhiteSpace(fieldValue))
                return ValidationResult.Invalid(PropertyName, $"{PropertyName} can not be blank");
            else
                return ValidationResult.OK(PropertyName);
        }
    }
}
