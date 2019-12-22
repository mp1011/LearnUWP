using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialDucks.Models.Transactions
{
    public interface ITransaction
    {
        FinancialEntity Source { get; }

        FinancialEntity Destination { get; }

        IEnumerable<DateTime> GetDates();
    }

    public abstract class TransactionBase<TFrom,TTo> : ITransaction 
        where TFrom:FinancialEntity
        where TTo:FinancialEntity
    {
        public TFrom Source { get; }

        public TTo Destination { get; }

        FinancialEntity ITransaction.Source => Source;

        FinancialEntity ITransaction.Destination => Destination;

        protected TransactionBase(TFrom source, TTo destination)
        {
            Source = source;
            Destination = destination;
        }

        public abstract IEnumerable<DateTime> GetDates();
    }
}
