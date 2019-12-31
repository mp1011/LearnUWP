using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Transactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace FinancialDucks.Models.UserData
{
    public class UserFinances : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<FinancialEntity> _userFinancialEntities = new List<FinancialEntity>();

        private List<ITransactionSchedule> _transactionSchedules = new List<ITransactionSchedule>();

        public FinancialEntity[] FinancialEntities => _userFinancialEntities.ToArray();


        public BankAccount[] BankAccounts => _userFinancialEntities.OfType<BankAccount>().ToArray();

        public Paycheck[] Paychecks => _userFinancialEntities.OfType<Paycheck>().ToArray();

        public GoodOrService[] Expenses => _userFinancialEntities.OfType<GoodOrService>().ToArray();

        public ITransactionSchedule[] TransactionSchedules => _transactionSchedules.ToArray();

        private bool _reloadRequired = false;
        public bool ReloadRequired
        {
            get => _reloadRequired;
            set
            {
                _reloadRequired = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ReloadRequired)));
            }
        }

        public void Add(params FinancialEntity[] entities)
        {
            foreach (var entity in entities)
            {
                if (entity.ID > 0)
                {
                    _userFinancialEntities.RemoveAll(p => p.ID == entity.ID && p.GetType() == entity.GetType());
                    _userFinancialEntities.Add(entity);
                }
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FinancialEntities)));
        }

        public void Add(params ITransactionSchedule[] schedules)
        {
            foreach (var schedule in schedules)
            {
                if (schedule.ID > 0)
                {
                    _userFinancialEntities.RemoveAll(p => p.ID == schedule.ID && p.GetType() == schedule.GetType());
                    _transactionSchedules.Add(schedule);
                }
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TransactionSchedules)));
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
                .Where(p => p.Source == entity || p.Destination == entity);
        }
    }
}
