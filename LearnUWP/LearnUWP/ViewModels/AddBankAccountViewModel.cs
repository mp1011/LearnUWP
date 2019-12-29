using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.ModelStorageServices;
using FinancialDucks.Services.UserServices;

namespace LearnUWP.ViewModels
{
    public class AddBankAccountViewModel : FinancialEntityCreateOrUpdateViewModel<BankAccount>
    {
        private readonly BankAccountStorageService _bankAccountStorageService;

        private BankAccountDataModel _dataModel;

        public string BankAccountName
        {
            get => _dataModel.Name;
            set
            {
                _dataModel.Name = value;
                InvokePropertyChange(nameof(BankAccountName));
            }
        }

        public AddBankAccountViewModel(BankAccountStorageService bankAccountStorageService,
            IUserSessionManager userSessionManager, StorageService storageService)
            :base(userSessionManager,storageService)
        {
            _bankAccountStorageService = bankAccountStorageService;
        }

        protected override void SetDataModels(StorageService storageService, BankAccount bankAccount)
        {
            _dataModel = _bankAccountStorageService.ToDataModel(bankAccount);
        }

        protected override BankAccount CreateOrUpdate(StorageService storageService)
        {
            var newBankAccount = _bankAccountStorageService.FromDataModel(_dataModel);
            var savedModel = _bankAccountStorageService.Store(storageService, newBankAccount);
            return savedModel;

        }
    }
}
