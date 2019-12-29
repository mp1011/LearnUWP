using FinancialDucks.Data.Models;
using FinancialDucks.Models;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class BankAccountStorageService
        : SingleModelStorageService<BankAccount, BankAccountDataModel>
    {
        public override BankAccount FromDataModel(BankAccountDataModel dataModel)
        {
            return new BankAccount(dataModel.ID, dataModel.Name, dataModel.InitialAmount);
        }

        public override BankAccount CreateNew()
        {
            return new BankAccount(0, string.Empty, 0);
        }

        public override BankAccountDataModel ToDataModel(BankAccount model)
        {
            return new BankAccountDataModel
            {
                ID = model.ID,
                Name = model.Name,
                InitialAmount = model.InitialAmount
            };
        }
    }
}
