using FinancialDucks.Data.Interfaces;
using FinancialDucks.Data.Models;
using FinancialDucks.Interfaces;

namespace FinancialDucks.Models
{
    public class BankAccount : FinancialEntity, IStoreable<BankAccountDataModel>
    {
        public BankAccount(string name, decimal initialAmount) : base(name, initialAmount)
        {

        }

        public void SetFrom(BankAccountDataModel dataModel)
        {
            ID = dataModel.ID;
            Name = dataModel.Name;
            InitialAmount = dataModel.InitialAmount;
        }

        public BankAccountDataModel ToDataModel()
        {
            return new BankAccountDataModel
            {
                ID = ID,
                Name = Name,
                InitialAmount = InitialAmount
            };
        }
    }
}
