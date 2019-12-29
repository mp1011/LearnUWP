using FinancialDucks.Data.Services;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.UserData;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class UserFinancesStorageService : IModelStorageService<UserFinances>
    {

        public UserFinances Load(StorageService storageService, int id)
        {
            var userFinances = new UserFinances();
           
            foreach (var bankAccount in storageService.LoadModels<BankAccount>())
                userFinances.AddEntity(bankAccount);

            foreach (var paycheck in storageService.LoadModels<Paycheck>())
                userFinances.AddEntity(paycheck);

            foreach (var expense in storageService.LoadModels<GoodOrService>())
                userFinances.AddEntity(expense);

            return userFinances;
        }

        public UserFinances[] LoadAllForUser(StorageService storageService, int userID)
        {
            return new UserFinances[] { Load(storageService, userID) };
        }

        public void Store(StorageService storageService, UserFinances model)
        {
            //not needed
        }
    }
}
