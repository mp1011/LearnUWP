using FinancialDucks.Models;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class AddPaycheckViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IUserSessionManager _sessionManager;
        private readonly DateService _dateService;

        private string _companyName;
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (_companyName != value)
                {
                    _companyName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CompanyName)));
                }
            }
        }

        private PayCycle _payCycle;
        public PayCycle PayCycle
        {
            get => _payCycle;
            set
            {
                if (_payCycle != value)
                {
                    _payCycle = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PayCycle)));
                }
            }
        }

        private DateTimeOffset _firstPayDate;
        public DateTimeOffset FirstPayDate
        {
            get => _firstPayDate;
            set
            {
                if (_firstPayDate != value)
                {
                    _firstPayDate = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstPayDate)));
                }
            }
        }

        private BankAccount _depositBank;
        public BankAccount DepositBank
        {
            get => _depositBank;
            set
            {
                if (_depositBank != value)
                {
                    _depositBank = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DepositBank)));
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

        public AddPaycheckViewModel(IUserSessionManager sessionManager, DateService dateService)
        {
            _sessionManager = sessionManager;
            _dateService = dateService;
            FirstPayDate = DateTime.Now;
        }

        public void Initialize()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();
            BankAccounts = new ObservableCollection<BankAccount>(userFinances.BankAccounts);
        }

        public void AddPaycheck()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();

            var startDate = FirstPayDate.DateTime;
            var paycheck = new Paycheck(
                companyName: CompanyName,
                initialAmount: Amount);

            userFinances.AddEntity(paycheck);
            userFinances.AddTransactionSchedule
            (
                new TransactionSchedule(paycheck, DepositBank,
                _dateService.CreateRecurrence(startDate, startDate.AddYears(100), PayCycle))
            );
        }
    }
}
