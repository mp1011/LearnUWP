using FinancialDucks.Models.Transactions;
using FinancialDucks.Models.UserData;
using System.Linq;

namespace FinancialDucks.Models
{
    public class Paycheck : FinancialEntity
    {
        public string CompanyName
        {
            get
            {
                return Name.Replace(" paycheck", "");
            }
        }

        public Paycheck(int id, string companyName, decimal initialAmount) 
            : base(id, $"{companyName} paycheck",initialAmount)
        {
        }

        public IncomeSchedule GetIncomeSchedule(UserFinances userFinances)
        {
            //todo, will eventually need to support multiple
            return userFinances.GetTransactionSchedulesFor(this)
                .OfType<IncomeSchedule>()
                .FirstOrDefault();
        }

    }
}
