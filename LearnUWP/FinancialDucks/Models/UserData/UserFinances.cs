using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Models.UserData
{
    public class UserFinances
    {
        private List<FinancialEntity> _userFinancialEntities = new List<FinancialEntity>();

        public BankAccount[] BankAccounts => _userFinancialEntities.OfType<BankAccount>().ToArray();

        public void AddEntity(FinancialEntity entity)
        {
            _userFinancialEntities.Add(entity);
        }
    }
}
