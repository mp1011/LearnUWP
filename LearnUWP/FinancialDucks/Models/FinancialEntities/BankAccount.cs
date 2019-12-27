using FinancialDucks.Data.Interfaces;
using FinancialDucks.Data.Models;
using FinancialDucks.Interfaces;
using System;

namespace FinancialDucks.Models
{
    public class BankAccount : FinancialEntity<BankAccountDataModel>
    {
        public BankAccount() { }
        public BankAccount(string name, decimal initialAmount)
        {
            Name = name;
            InitialAmount=initialAmount;
        }

        public override void SetFrom(BankAccountDataModel dataModel)
        {
            ID = dataModel.ID;
            Name = dataModel.Name;
            InitialAmount = dataModel.InitialAmount;
        }

        public override BankAccountDataModel ToDataModel()
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
