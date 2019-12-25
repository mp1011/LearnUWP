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
    public class AddExpenseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IUserSessionManager _sessionManager;
        private readonly DateService _dateService;

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WithdrawalBank)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RecurrenceType)));
                }
            }
        }

        public ObservableCollection<BankAccount> BankAccounts { get; private set; }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Amount)));
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
                    _firstPayDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PayDate)));
                }
            }
        }



        public AddExpenseViewModel(IUserSessionManager sessionManager, DateService dateService)
        {
            _sessionManager = sessionManager;
            _dateService = dateService;
            PayDate = DateTime.Now;
        }

        public void Initialize()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();
            BankAccounts = new ObservableCollection<BankAccount>(userFinances.BankAccounts);
        }

        public void AddExpense()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();

            var payDate = PayDate.DateTime;
            var expense = new GoodOrService(
                description: Description,
                initialAmount: Amount);

            userFinances.AddEntity(expense);
            userFinances.AddTransactionSchedule
            (
                new TransactionSchedule(expense, WithdrawalBank,
                _dateService.CreateRecurrence(payDate, payDate.AddYears(100), RecurrenceType))
            );
        }
    }
}
