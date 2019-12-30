using FinancialDucks.Data.Services;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Models.UserData;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class UserFinancesStorageService : IModelStorageService<UserFinances>
    {
        public UserFinances CreateNew()
        {
            return new UserFinances();
        }

        public UserFinances Load(StorageService storageService, int id)
        {
            var userFinances = new UserFinances();

            userFinances.Add(storageService.LoadModelsForUser<BankAccount>(id));

            userFinances.Add(storageService.LoadModelsForUser<Paycheck>(id));

            userFinances.Add(storageService.LoadModelsForUser<GoodOrService>(id));

            userFinances.Add(storageService.LoadModelsForUser<IncomeSchedule>(id));
        
            return userFinances;
        }

        public UserFinances[] LoadAllForUser(StorageService storageService, int userID)
        {
            return new UserFinances[] { Load(storageService, userID) };
        }

        public UserFinances Store(StorageService storageService, UserFinances model)
        {
            //not needed
            return model;            
        }
    }
}
