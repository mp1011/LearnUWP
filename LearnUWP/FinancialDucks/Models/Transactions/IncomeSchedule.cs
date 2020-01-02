using FinancialDucks.Services;
using System;

namespace FinancialDucks.Models.Transactions
{
    public class IncomeSchedule :
        TransactionSchedule<Paycheck, BankAccount>
    {
        public PayCycle PayCycle { get; }

        public DateTime FirstDate { get; }

        public DateTime LastDate { get; }

        public IncomeSchedule(int id, Paycheck paycheck, BankAccount bankAccount, PayCycle payCycle, DateTime firstDate, DateTime lastDate, RecurrenceFactory recurrenceFactory)
            : base(id, paycheck, bankAccount, CreateRecurrence(recurrenceFactory, payCycle, firstDate, lastDate))
        {
            PayCycle = payCycle;
            FirstDate = firstDate;
            LastDate = lastDate;
        }

        private static Recurrence CreateRecurrence(RecurrenceFactory recurrenceFactory, PayCycle payCycle, DateTime firstDate, DateTime lastDate)
        {
            return recurrenceFactory.CreateRecurrence(
                firstDate,
                lastDate,
                recurrenceFactory.CreatePeriod(firstDate, payCycle));
        }

        public override FinancialTransaction CreateTransfer(DateTime date)
        {
            return new PercentTransfer
            (
                source: Source,
                destination: Destination,
                date: date,
                percent: 1.0M,
                isPercentOfSource: true,
                addToDestination: true,
                subtractFromSource: false
            );
        }
    }
}
