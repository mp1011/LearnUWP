using FinancialDucks.Models;
using FinancialDucks.Models.UserData;
using System.Security.Authentication;

namespace FinancialDucks.Services.UserServices
{
    /// <summary>
    /// meant for testing, has no persistence, holds all values in memory, and only supports one user
    /// </summary>
    public class SingleUserInMemoryUserSessionManager : IUserSessionManager
    {    
        public int CurrentUserID { get; private set; }

        private UserFinances _currentUserFinances;

        public UserFinances CurrentUserFinances
        {
            get => _currentUserFinances ?? throw new AuthenticationException("No user is logged in");
            set => _currentUserFinances=value;
        }

        private readonly StorageService _storageService;

        public SingleUserInMemoryUserSessionManager(StorageService storageService)
        {
            _storageService = storageService;
        }

        public void Login(int userID)
        {
            CurrentUserID = userID;
            CurrentUserFinances = _storageService.LoadModel<UserFinances>(CurrentUserID);
        }
    }
}
