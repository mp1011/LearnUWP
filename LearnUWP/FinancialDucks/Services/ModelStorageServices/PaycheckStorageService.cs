using FinancialDucks.Data.Models;
using FinancialDucks.Models;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class PayCheckStorageService
        : SingleModelStorageService<Paycheck, PaycheckDataModel>
    {
        protected override Paycheck FromDataModel(PaycheckDataModel dataModel)
        {
            return new Paycheck(dataModel.ID, dataModel.CompanyName, dataModel.InitialAmount);
        }

        protected override PaycheckDataModel ToDataModel(Paycheck model)
        {
            return new PaycheckDataModel
            {
                ID = model.ID,
                CompanyName = model.Name,
                InitialAmount = model.InitialAmount
            };
        }
    }
}
