using System;
using System.Collections.Generic;

namespace FinancialDucks.Models.Transactions
{
    public interface ITransactionSchedule
    {
        Recurrence Recurrence { get;}

        FinancialEntity Source { get; }

        FinancialEntity Destination { get; }
    }

    public abstract class TransactionSchedule<TSource,TDestination> : ITransactionSchedule
        where TSource:FinancialEntity
        where TDestination:FinancialEntity
    {
        public int ID { get; protected set; }
        public TSource Source { get; protected set; }

        public TDestination Destination { get; protected set; }


        public Recurrence Recurrence { get; private set; }

        FinancialEntity ITransactionSchedule.Source => Source;

        FinancialEntity ITransactionSchedule.Destination => Destination;

        public TransactionSchedule(TSource source, TDestination destination, Recurrence recurrence)
        {
            Source = source;
            Destination = destination;
            Recurrence = recurrence;
        }

        public TransactionSchedule()
        {
        }

    
    }
}
