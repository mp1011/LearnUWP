using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Services;
using FinancialDucks.Services.UserServices;

namespace LearnUWP.ViewModels
{
    public class AddBankAccountViewModel : CreateOrEditViewModel<BankAccount>
    {
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

        public AddBankAccountViewModel(IUserSessionManager sessionManager, StorageService storageService)
            :base(sessionManager, storageService)
        {
        }
    }
}
