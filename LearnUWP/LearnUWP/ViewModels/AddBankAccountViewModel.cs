using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;
using System.ComponentModel;

namespace LearnUWP.ViewModels
{
    public class AddBankAccountViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IUserSessionManager _sessionManager;
        private readonly StorageService _storageService;

        private BankAccountDataModel _model;

        public string SaveActionName => _model.ID > 0 ? "Save" : "Create";

        public string BankAccountName
        {
            get => _model.Name;
            set
            {
                _model.Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BankAccountName)));
            }
        }

        public AddBankAccountViewModel(IUserSessionManager sessionManager, StorageService storageService)
        {
            _sessionManager = sessionManager;
            _storageService = storageService;
        }

        public void Initialize(BankAccount bankAccount)
        {
            if (bankAccount != null)
            {
                _model = bankAccount.ToDataModel();
            }
            else
            {
                _model = new BankAccount().ToDataModel();
            }
          
        }

        public void AddBankAccount()
        {
            var userFinances = _sessionManager.GetCurrentUserFinances();

            var bankAccount = userFinances.TryGetEntity<BankAccount>(_model.ID);
            if(bankAccount == null)
            {
                bankAccount = new BankAccount();
                userFinances.AddEntity(bankAccount);
            }

            bankAccount.SetFrom(_model);
            _storageService.StoreModel(bankAccount);
        }
    }
}
