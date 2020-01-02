using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Services;
using System;

namespace FinancialDucks.Models.Transactions
{
    public class PaymentSchedule : TransactionSchedule<BankAccount,GoodOrService>
    {
        public RecurrenceType RecurrenceType { get; }
        public AmountType AmountType { get; }

        public decimal Amount { get; }

        public DateTime FirstDate { get; }

        public DateTime LastDate { get; }

        public string Description { get; }

        public PaymentSchedule(int id, BankAccount bankAccount, GoodOrService expense, RecurrenceType recurrenceType, 
            AmountType amountType, decimal amount, string description,
            DateTime firstDate, DateTime lastDate, RecurrenceFactory recurrenceFactory)
            : base(id, bankAccount, expense, CreateRecurrence(recurrenceFactory, recurrenceType, firstDate, lastDate))
        {
            RecurrenceType = recurrenceType;
            FirstDate = firstDate;
            LastDate = lastDate;
            AmountType = amountType;
            Amount = amount;
            Description = description;
        }

        private static Recurrence CreateRecurrence(RecurrenceFactory recurrenceFactory, RecurrenceType recurrenceType, DateTime firstDate, DateTime lastDate)
        {
            return recurrenceFactory.CreateRecurrence(
                firstDate,
                lastDate,
                recurrenceFactory.CreatePeriod(firstDate, recurrenceType));
        }

        public override FinancialTransaction CreateTransfer(DateTime date)
        {
            //todo, make more polymorphic
            if (AmountType == AmountType.Exact)
            {
                return new MoneyTransfer(Source, Destination, date, Amount);
            }
            else if (AmountType == AmountType.Percent)
            {
                return new PercentTransfer(Source, Destination, date, Amount, false, true, false);
            }
            else
                throw new ArgumentException($"Unexpected amount type {AmountType}");
        }
    }
}
