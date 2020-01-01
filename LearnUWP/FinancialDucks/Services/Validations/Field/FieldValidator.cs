using FinancialDucks.Models;

namespace FinancialDucks.Services.Validations
{
    public interface IFieldValidator
    {
        string PropertyName { get; }

    }

    public interface IFieldValidator<TModel> : IFieldValidator
    {
        ValidationResult Validate(TModel model);
    }

    public abstract class FieldValidator<TModel, TField> : IFieldValidator<TModel>
    {
        public abstract string PropertyName { get; }
       
        public ValidationResult Validate(TModel model)
        {
            TField propertyValue = (TField)typeof(TModel)
                .GetProperty(PropertyName)
                .GetValue(model);

            return Validate(propertyValue);
        }

        protected abstract ValidationResult Validate(TField fieldValue);
    }
}
