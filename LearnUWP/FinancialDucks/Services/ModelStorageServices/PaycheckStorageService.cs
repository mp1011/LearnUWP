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
                CompanyName = model.CompanyName,
                InitialAmount = model.InitialAmount
            };
        }

        public override void DeleteModelAndDependencies(StorageService storageService, Paycheck model)
        {
            storageService.DAO.Delete<IncomeScheduleDataModel>("PaycheckID=@ID", new { model.ID });
            storageService.DAO.Delete<PaycheckDataModel>("ID=@ID", new { model.ID });
        }
    }
}
