using FinancialDucks.Models.UserData;

namespace FinancialDucks.Services.UserServices
{
    public interface IUserSessionManager
    {
        UserFinances GetCurrentUserFinances();
    }

}
