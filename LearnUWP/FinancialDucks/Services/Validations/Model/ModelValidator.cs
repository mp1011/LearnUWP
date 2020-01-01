using FinancialDucks.Models;
using FinancialDucks.Services.Validations.Field;
using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Services.Validations.Model
{
    public interface IModelValidator
    {

    }

    public abstract class ModelValidator<T> : IModelValidator
    {
        private IFieldValidator<T>[] _fieldValidators;

        public ModelValidator()
        {
            _fieldValidators = GetPropertyValidators().ToArray();
        }

        public ValidationResult[] Validate(T itemToValidate)
        {
            var result = _fieldValidators
                .Select(v => v.Validate(itemToValidate))
                .ToArray();

            return result;
        }

        protected abstract IEnumerable<IFieldValidator<T>> GetPropertyValidators();
    }
}
