using FinancialDucks.Data.Models;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Models.UserData;
using System.Linq;

namespace FinancialDucks.Models.FinancialEntities
{
    public class GoodOrService : FinancialEntity
    {

        public GoodOrService(int id, string description, decimal initialAmount)
            :base(id, description, initialAmount)
        {
        }

        public PaymentSchedule GetPaymentSchedule(UserFinances userFinances)
        {
            //todo, will eventually need to support multiple
            return userFinances.GetTransactionSchedulesFor(this)
                .OfType<PaymentSchedule>()
                .FirstOrDefault();
        }

    }
}
