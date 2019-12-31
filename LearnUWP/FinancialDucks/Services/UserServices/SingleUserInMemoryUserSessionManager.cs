using FinancialDucks.Models;
using FinancialDucks.Models.UserData;
using System.ComponentModel;
using System.Security.Authentication;

namespace FinancialDucks.Services.UserServices
{
    /// <summary>
    /// meant for testing, has no persistence, holds all values in memory, and only supports one user
    /// </summary>
    public class SingleUserInMemoryUserSessionManager : IUserSessionManager
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public int CurrentUserID { get; private set; }

        private UserFinances _currentUserFinances;

        public UserFinances CurrentUserFinances
        {
            get => _currentUserFinances ?? throw new AuthenticationException("No user is logged in");
            set
            {
                _currentUserFinances = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentUserFinances)));
            }
           
        }

        private readonly StorageService _storageService;


        public SingleUserInMemoryUserSessionManager(StorageService storageService)
        {
            _storageService = storageService;
        }

        public void Login(int userID)
        {
            CurrentUserID = userID;
            ReloadFinances();
        }

        private void ReloadFinances()
        {
            //is it safe to replace the object like this?
            CurrentUserFinances = _storageService.LoadModel<UserFinances>(CurrentUserID);
            CurrentUserFinances.PropertyChanged += CurrentUserFinances_PropertyChanged;
        }

        private void CurrentUserFinances_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserFinances.ReloadRequired))
            {
                if(CurrentUserFinances.ReloadRequired)
                    ReloadFinances();
            }
        }
    }
}
