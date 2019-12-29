using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class ExpenseStorageService
        : SingleModelStorageService<GoodOrService, ExpensesDataModel>
    {
        public override GoodOrService FromDataModel(StorageService storageService, ExpensesDataModel dataModel)
        {
            return new GoodOrService(dataModel.ID, dataModel.Description, dataModel.InitialAmount);
        }

        public override GoodOrService CreateNew()
        {
            return new GoodOrService(0, string.Empty, 0);
        }

        public override ExpensesDataModel ToDataModel(StorageService storageService, GoodOrService model)
        {
            return new ExpensesDataModel
            {
                ID = model.ID,
                Description = model.Name,
                InitialAmount = model.InitialAmount
            };
        }
    }
}
