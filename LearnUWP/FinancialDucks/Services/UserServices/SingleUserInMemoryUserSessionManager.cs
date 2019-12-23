using FinancialDucks.Models.UserData;

namespace FinancialDucks.Services.UserServices
{
    /// <summary>
    /// meant for testing, has no persistence, holds all values in memory, and only supports one user
    /// </summary>
    public class SingleUserInMemoryUserSessionManager : IUserSessionManager
    {
        private UserFinances _userFinances = new UserFinances();

        public UserFinances GetCurrentUserFinances()
        {
            return _userFinances;
        }
    }
}
