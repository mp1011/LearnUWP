using FinancialDucks.Models;
using FinancialDucks.Services.UserServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IUserSessionManager _sessionManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BankAccount> BankAccounts { get; private set; }
        public ObservableCollection<Paycheck> Paychecks { get; private set; }

        public MainPageViewModel(IUserSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
            Initialize();
        }

        public void Initialize()
        {
            BankAccounts = new ObservableCollection<BankAccount>(_sessionManager.GetCurrentUserFinances().BankAccounts);
            Paychecks = new ObservableCollection<Paycheck>(_sessionManager.GetCurrentUserFinances().Paychecks);

        }
    }
}
