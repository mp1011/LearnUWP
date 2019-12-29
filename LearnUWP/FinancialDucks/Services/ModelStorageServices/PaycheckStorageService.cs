using FinancialDucks.Data.Models;
using FinancialDucks.Models;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class PayCheckStorageService
        : SingleModelStorageService<Paycheck, PaycheckDataModel>
    {
        public override Paycheck FromDataModel(StorageService storageService, PaycheckDataModel dataModel)
        {
            return new Paycheck(dataModel.ID, dataModel.CompanyName, dataModel.InitialAmount);
        }

        public override Paycheck CreateNew()
        {
            return new Paycheck(0, string.Empty, 0);
        }

        public override PaycheckDataModel ToDataModel(StorageService storageService, Paycheck model)
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
