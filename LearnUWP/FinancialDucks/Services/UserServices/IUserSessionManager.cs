using FinancialDucks.Models.UserData;

namespace FinancialDucks.Services.UserServices
{
    public interface IUserSessionManager
    {
        UserFinances CurrentUserFinances { get; }
        int CurrentUserID { get; }
        void Login(int userID);
    }

}
