using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.ModelStorageServices;
using FinancialDucks.Services.UserServices;
using LearnUWP.Services;

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
            IUserSessionManager userSessionManager, StorageService storageService, ValidationService validationService, NavigationService navigationService)
            :base(userSessionManager,storageService, validationService, navigationService)
        {
            _bankAccountStorageService = bankAccountStorageService;
        }

        protected override void SetDataModels(BankAccount bankAccount)
        {
            _dataModel = _bankAccountStorageService.ToDataModel(StorageService, bankAccount);
        }

        public override BankAccount SaveModel()
        {
            var newBankAccount = _bankAccountStorageService.FromDataModel(StorageService, _dataModel);
            var savedModel = _bankAccountStorageService.Store(StorageService, newBankAccount);
            SessionManager.CurrentUserFinances.Add(savedModel);
            return savedModel;
        }

        public override ValidationResult[] Validate()
        {
            return ValidationService.ValidateModel(_dataModel ?? new BankAccountDataModel());
        }
    }
}
