using FinancialDucks.Models;
using FinancialDucks.Services.UserServices;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class AddBankAccountViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private IUserSessionManager _sessionManager;

        private string _bankAccountName;
        public string BankAccountName
        {
            get => _bankAccountName;
            set
            {
                _bankAccountName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BankAccountName)));
            }
        }

        public AddBankAccountViewModel(IUserSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void AddBankAccount()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();
            var bank = new BankAccount(BankAccountName);
            userFinances.AddEntity(bank);
        }
    }
}
