using FinancialDucks.Data.Models;
using FinancialDucks.Services.Validations.Field;
using System.Collections.Generic;

namespace FinancialDucks.Services.Validations.Model
{
    public class PaycheckValidator : ModelValidator<PaycheckDataModel>
    {
        protected override IEnumerable<IFieldValidator<PaycheckDataModel>> GetPropertyValidators()
        {
            yield return new GreaterThanZeroValidator<PaycheckDataModel>(nameof(PaycheckDataModel.InitialAmount));
            yield return new NotEmptyValidator<PaycheckDataModel>(nameof(PaycheckDataModel.CompanyName));
        }
    }
}
