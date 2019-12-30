using System;
using System.Collections.Generic;

namespace FinancialDucks.Models.Transactions
{
    public interface ITransactionSchedule
    {
        int ID { get; }
        Recurrence Recurrence { get;}

        FinancialEntity Source { get; }

        FinancialEntity Destination { get; }
    }

    public abstract class TransactionSchedule<TSource,TDestination> : ITransactionSchedule
        where TSource:FinancialEntity
        where TDestination:FinancialEntity
    {
        public int ID { get; }
        public TSource Source { get;  }

        public TDestination Destination { get;  }


        public Recurrence Recurrence { get; }

        FinancialEntity ITransactionSchedule.Source => Source;

        FinancialEntity ITransactionSchedule.Destination => Destination;

        public TransactionSchedule(int id, TSource source, TDestination destination, Recurrence recurrence)
        {
            ID = id;
            Source = source;
            Destination = destination;
            Recurrence = recurrence;
        }
    
    }
}
