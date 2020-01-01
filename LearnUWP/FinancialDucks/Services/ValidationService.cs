using FinancialDucks.Models;
using FinancialDucks.Services.Validations;
using FinancialDucks.Services.Validations.Model;
using System;
using System.Linq;

namespace FinancialDucks.Services
{
    public class ValidationService
    {
        private readonly IModelValidator[] _modelValidators;

        public ValidationService(IModelValidator[] modelValidators)
        {
            _modelValidators = modelValidators;
        }

        public ValidationResult[] ValidateModel<T>(T model)
        {
            var validationResults = _modelValidators
                .OfType<ModelValidator<T>>()
                .SelectMany(v => v.Validate(model))
                .ToArray();

            return validationResults;
        }
    }
}
