using FinancialDucks.Data.Models;
using FinancialDucks.Services.Validations.Field;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDucks.Services.Validations.Model
{
    public class ExpenseValidator : ModelValidator<ExpensesDataModel>
    {
        protected override IEnumerable<IFieldValidator<ExpensesDataModel>> GetPropertyValidators()
        {
            yield return new GreaterThanZeroValidator<ExpensesDataModel>(nameof(ExpensesDataModel.InitialAmount));
            yield return new NotEmptyValidator<ExpensesDataModel>(nameof(ExpensesDataModel.Description));
        }
    }
}
