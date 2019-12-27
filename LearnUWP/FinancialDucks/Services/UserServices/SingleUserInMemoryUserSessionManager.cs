using FinancialDucks.Models;
using FinancialDucks.Models.UserData;

namespace FinancialDucks.Services.UserServices
{
    /// <summary>
    /// meant for testing, has no persistence, holds all values in memory, and only supports one user
    /// </summary>
    public class SingleUserInMemoryUserSessionManager : IUserSessionManager
    {
        private readonly UserFinances _userFinances;

        public SingleUserInMemoryUserSessionManager(StorageService storageService)
        {
            _userFinances = new UserFinances();

            foreach(var bankAccount in storageService.LoadModels<BankAccount>())
                _userFinances.AddEntity(bankAccount);
        }

        public UserFinances GetCurrentUserFinances()
        {
            return _userFinances;
        }
    }
}
