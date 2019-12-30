using FinancialDucks.Models.UserData;
using System.ComponentModel;

namespace FinancialDucks.Services.UserServices
{
    public interface IUserSessionManager : INotifyPropertyChanged
    {
        UserFinances CurrentUserFinances { get; }
        int CurrentUserID { get; }
        void Login(int userID);
    }

}
