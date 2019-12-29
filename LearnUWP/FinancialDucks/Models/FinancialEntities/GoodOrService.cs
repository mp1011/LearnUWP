using FinancialDucks.Data.Models;

namespace FinancialDucks.Models.FinancialEntities
{
    public class GoodOrService : FinancialEntity
    {

        public GoodOrService(int id, string description, decimal initialAmount)
            :base(id, description, initialAmount)
        {
        }

    }
}
