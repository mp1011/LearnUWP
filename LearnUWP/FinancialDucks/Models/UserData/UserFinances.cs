using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialDucks.Models.UserData
{
    public class UserFinances
    {
        private List<FinancialEntity> _userFinancialEntities = new List<FinancialEntity>();

        private List<ITransactionSchedule> _transactionSchedules = new List<ITransactionSchedule>();

        public BankAccount[] BankAccounts => _userFinancialEntities.OfType<BankAccount>().ToArray();

        public Paycheck[] Paychecks => _userFinancialEntities.OfType<Paycheck>().ToArray();

        public GoodOrService[] Expenses => _userFinancialEntities.OfType<GoodOrService>().ToArray();

        public ITransactionSchedule[] TransactionSchedules => _transactionSchedules.ToArray();


        public void AddEntity(FinancialEntity entity)
        {
            //todo - check if observables play nice
            if (entity.ID > 0)
                _userFinancialEntities.RemoveAll(p => p.ID == entity.ID && p.GetType() == entity.GetType());
            _userFinancialEntities.Add(entity);
        }

        public void AddTransactionSchedule(ITransactionSchedule schedule)
        {
            _transactionSchedules.Add(schedule);
        }

        public T TryGetEntity<T>(int ID)
            where T:FinancialEntity
        {
            return _userFinancialEntities
                .OfType<T>()
                .FirstOrDefault(p => p.ID == ID);
        }

        public FinancialEntity[] GetEntities(Type entityType)
        {
            return _userFinancialEntities
                .Where(p => p.GetType().IsAssignableFrom(entityType))
                .ToArray();
        }

        public IEnumerable<ITransactionSchedule> GetTransactionSchedulesFor(FinancialEntity entity)
        {
            return _transactionSchedules
                .Where(p => p.Source == entity);
        }
    }
}
