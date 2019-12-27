using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Services.UserServices;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class AddBankAccountViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IUserSessionManager _sessionManager;

        private BankAccountDataModel _model;

        public string BankAccountName
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BankAccountName)));
            }
        }

        public AddBankAccountViewModel(IUserSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public void Initialize(BankAccount bankAccount)
        {
            if (bankAccount != null)
            {
                _model = new BankAccountDataModel
                {
                    ID = bankAccount.ID,
                    InitialAmount = bankAccount.InitialAmount,
                    Name = bankAccount.Name
                };
            }
            else
            {
                _model = new BankAccountDataModel
                {
                    Name = string.Empty
                };
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
