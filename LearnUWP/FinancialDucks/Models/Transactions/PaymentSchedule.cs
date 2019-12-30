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
    }
}
