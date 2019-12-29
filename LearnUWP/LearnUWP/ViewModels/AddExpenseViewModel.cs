using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class AddExpenseViewModel : FinancialEntityCreateOrUpdateViewModel<GoodOrService>
    {
        private ExpensesDataModel _dataModel;

        public string Description
        {
            get => _dataModel.Description;
            set
            {
                if (_dataModel.Description != value)
                {
                    _dataModel.Description = value;
                    InvokePropertyChange(nameof(Description));
                }
            }
        }

      
        private BankAccount _withdrawalBank;
        public BankAccount WithdrawalBank
        {
            get => _withdrawalBank;
            set
            {
                if (_withdrawalBank != value)
                {
                    _withdrawalBank = value;
                    throw new System.NotImplementedException();
                  //  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WithdrawalBank)));
                }
            }
        }

        private RecurrenceType _recurrenceType;
        public RecurrenceType RecurrenceType
        {
            get => _recurrenceType;
            set
            {
                if(_recurrenceType != value)
                {
                    _recurrenceType = value;

                    throw new System.NotImplementedException();
                    //  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RecurrenceType)));
                }
            }
        }

        public ObservableCollection<BankAccount> BankAccounts { get; private set; }

        public decimal Amount
        {
            get => _dataModel.InitialAmount;
            set
            {
                if (_dataModel.InitialAmount != value)
                {
                    _dataModel.InitialAmount = value;
                    InvokePropertyChange(nameof(Amount));
                }
            }
        }

        private DateTimeOffset _firstPayDate;
        public DateTimeOffset PayDate
        {
            get => _firstPayDate;
            set
            {
                if (_firstPayDate != value)
                {
                    _firstPayDate = value; throw new System.NotImplementedException();
                    // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PayDate)));
                }
            }
        }



        public AddExpenseViewModel(IUserSessionManager sessionManager, StorageService storageService, DateService dateService) 
            :base(sessionManager,storageService)
        {
            throw new System.NotImplementedException("date service?");
            PayDate = DateTime.Now;
        }
        protected override void SetDataModels(StorageService storageService, GoodOrService model)
        {
            throw new NotImplementedException();
        }

        protected override GoodOrService CreateOrUpdate(StorageService storageService)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
            //var userFinances = _sessionManager.GetCurrentUserFinances();
            //BankAccounts = new ObservableCollection<BankAccount>(userFinances.BankAccounts);
        }

        //todo - replace this
        //public void AddExpense()
        //{
        //    var userFinances = _sessionManager.GetCurrentUserFinances();

        //    var payDate = PayDate.DateTime;
        //    var expense = new GoodOrService(
        //        description: Description,
        //        initialAmount: Amount);

        //    userFinances.AddEntity(expense);
        //    userFinances.AddTransactionSchedule
        //    (
        //        new TransactionSchedule(expense, WithdrawalBank,
        //        _dateService.CreateRecurrence(payDate, payDate.AddYears(100), RecurrenceType))
        //    );
        //}
    }
}
