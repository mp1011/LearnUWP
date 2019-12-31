using FinancialDucks.Data.Models;
using FinancialDucks.Models;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class BankAccountStorageService
        : SingleModelStorageService<BankAccount, BankAccountDataModel>
    {
        public override BankAccount FromDataModel(StorageService storageService, BankAccountDataModel dataModel)
        {
            return new BankAccount(dataModel.ID, dataModel.Name, dataModel.InitialAmount);
        }

        public override BankAccount CreateNew()
        {
            return new BankAccount(0, string.Empty, 0);
        }

        public override BankAccountDataModel ToDataModel(StorageService storageService, BankAccount model)
        {
            return new BankAccountDataModel
            {
                ID = model.ID,
                Name = model.Name,
                InitialAmount = model.InitialAmount
            };
        }

        public override void DeleteModelAndDependencies(StorageService storageService, BankAccount model)
        {
            storageService.DAO.Delete<IncomeScheduleDataModel>("DepositBankID=@ID", new { model.ID });
            storageService.DAO.Delete<PaymentScheduleDataModel>("BankAccountID=@ID", new { model.ID });
            storageService.DAO.Delete<BankAccountDataModel>("ID=@ID", new { model.ID });
        }
    }
}
