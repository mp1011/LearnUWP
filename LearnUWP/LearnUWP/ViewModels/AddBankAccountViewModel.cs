using FinancialDucks.Models;
using FinancialDucks.Services.UserServices;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class AddBankAccountViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IUserSessionManager _sessionManager;

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

        public void Initialize(BankAccount bankAccount)
        {
            if(bankAccount == null)
            {
                BankAccountName = string.Empty;
            }
            else
            {
                BankAccountName = bankAccount.Name;
            }
        }

        public void AddBankAccount()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();
            var bank = new BankAccount(BankAccountName,0);
            userFinances.AddEntity(bank);
        }
    }
}
