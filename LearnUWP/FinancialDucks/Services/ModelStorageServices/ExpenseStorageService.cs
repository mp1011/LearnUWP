using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class ExpenseStorageService
        : SingleModelStorageService<GoodOrService, ExpensesDataModel>
    {
        protected override GoodOrService FromDataModel(ExpensesDataModel dataModel)
        {
            return new GoodOrService(dataModel.ID, dataModel.Description, dataModel.InitialAmount);
        }

        protected override ExpensesDataModel ToDataModel(GoodOrService model)
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
