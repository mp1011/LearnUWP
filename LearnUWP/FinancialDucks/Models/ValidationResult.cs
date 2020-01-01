using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Models
{
    public class ValidationResult
    {
        public string PropertyName { get; }
        public string Message { get; }

        public bool IsValid => string.IsNullOrEmpty(Message);

        private ValidationResult(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }

        public static ValidationResult Invalid(string propertyName, string message)
        {
            return new ValidationResult(propertyName, message);
        }

        public static ValidationResult OK(string propertyName)
        {
            return new ValidationResult(propertyName, null);
        }
    }

    public static class ValidationResultExtensions
    {
        public static void ThrowErrorIfInvalid(this IEnumerable<ValidationResult> results)
        {
            var exceptions = results
                .Where(p => !p.IsValid)
                .Select(p => new Exception(p.Message))
                .ToArray();

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions.First().Message, exceptions);
            }
        }
    }
}
