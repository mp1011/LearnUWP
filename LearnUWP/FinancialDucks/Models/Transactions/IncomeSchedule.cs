using FinancialDucks.Data.Models;
using System;

namespace FinancialDucks.Models.Transactions
{
    public class IncomeSchedule :
        TransactionSchedule<Paycheck, BankAccount>
    {
        public PayCycle PayCycle { get; private set; }

        public DateTime PaymentDate { get; private set; }

   
        public IncomeSchedule(Paycheck paycheck, BankAccount bankAccount, Recurrence recurrence) 
            :base(paycheck,bankAccount,recurrence)
        {

        }

        public IncomeSchedule() { }
    }
}
