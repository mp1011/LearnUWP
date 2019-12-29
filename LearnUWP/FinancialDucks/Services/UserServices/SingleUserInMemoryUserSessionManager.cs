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

        public int CurrentUserID => -1;

        public SingleUserInMemoryUserSessionManager()
        {
            //fix me

            //_userFinances = new UserFinances();
            ////todo, should be in a different class
            //foreach(var bankAccount in storageService.LoadModels<BankAccount>())
            //    _userFinances.AddEntity(bankAccount);
        }

        public UserFinances GetCurrentUserFinances()
        {
            return _userFinances;
        }
    }
}
