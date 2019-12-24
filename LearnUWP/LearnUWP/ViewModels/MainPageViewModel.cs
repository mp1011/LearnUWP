using FinancialDucks.Models;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LearnUWP.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IUserSessionManager _sessionManager;
        private readonly TransactionService _transactionService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ObservableCollection<Paycheck> Paychecks { get; private set; }

        public ObservableCollection<FinancialSnapshot> Timeline { get; private set; }

        public MainPageViewModel(IUserSessionManager sessionManager, TransactionService transactionService)
        {
            _sessionManager = sessionManager;
            _transactionService = transactionService;
            Initialize();
        }

        public void Initialize()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();
            BankAccounts = new ObservableCollection<BankAccount>(userFinances.BankAccounts);
            Paychecks = new ObservableCollection<Paycheck>(userFinances.Paychecks);

           Timeline = new ObservableCollection<FinancialSnapshot>();
        }
    }
}
