using FinancialDucks.Data.Models;
using FinancialDucks.Services.Validations.Field;
using System.Collections.Generic;

namespace FinancialDucks.Services.Validations.Model
{
    public class BankAccountValidator : ModelValidator<BankAccountDataModel>
    {
        protected override IEnumerable<IFieldValidator<BankAccountDataModel>> GetPropertyValidators()
        {
            yield return new NotEmptyValidator<BankAccountDataModel>(nameof(BankAccountDataModel.Name));
        }
    }
}
