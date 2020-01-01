using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FinancialDucks.Services.ModelStorageServices;
using FinancialDucks.Services.UserServices;
using LearnUWP.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LearnUWP.ViewModels
{
    public class AddPaycheckViewModel : FinancialEntityCreateOrUpdateViewModel<Paycheck>
    {
        private readonly PayCheckStorageService _payCheckStorageService;
        private readonly IncomeScheduleStorageService _incomeScheduleStorageService;

        private PaycheckDataModel _dataModel;
        private IncomeScheduleDataModel _incomeScheduleDataModel;

        public string CompanyName
        {
            get => _dataModel.CompanyName;
            set
            {
                if (_dataModel.CompanyName != value)
                {
                    _dataModel.CompanyName = value;
                    InvokePropertyChange(nameof(CompanyName));
                }
            }
        }

        public PayCycle PayCycle
        {
            get => (PayCycle)_incomeScheduleDataModel.PayCycleID;
            set
            {
                if (_incomeScheduleDataModel.PayCycleID != (int)value)
                {
                    _incomeScheduleDataModel.PayCycleID = (int)value;
                    InvokePropertyChange(nameof(PayCycle));
                }
            }
        }

        public DateTimeOffset FirstPayDate
        {
            get => _incomeScheduleDataModel.FirstPaymentDate;
            set
            {
                if (_incomeScheduleDataModel.FirstPaymentDate != value.DateTime)
                {
                    _incomeScheduleDataModel.FirstPaymentDate = value.DateTime;
                    InvokePropertyChange(nameof(FirstPayDate));
                }
            }
        }


        private BankAccount _depositBank = null;

        public BankAccount DepositBank
        {
            get => _depositBank;
            set
            {
                if (_depositBank != value && value != null)
                {
                    _depositBank = value;
                    _incomeScheduleDataModel.DepositBankID = value.ID;
                    InvokePropertyChange(nameof(DepositBank));
                }
            }
        }

        public decimal Amount
        {
            get => _dataModel.InitialAmount;
            set
            {
                if (_dataModel.InitialAmount != value)
                {
                    _dataModel.InitialAmount = value;
                    InvokePropertyChange(nameof(Amount));
                }
            }
        }

        public AddPaycheckViewModel(PayCheckStorageService payCheckStorageService, IncomeScheduleStorageService incomeScheduleStorageService,
            IUserSessionManager sessionManager, StorageService storageService, NavigationService navigationService, ValidationService validationService, RecurrenceFactory dateService)
            : base(sessionManager,storageService, validationService, navigationService)
        {
            _payCheckStorageService = payCheckStorageService;
            _incomeScheduleStorageService = incomeScheduleStorageService;
        }

        protected override void SetDataModels(Paycheck paycheck)
        {
            _dataModel = _payCheckStorageService.ToDataModel(StorageService, paycheck);

            var incomeSchedule = paycheck.GetIncomeSchedule(SessionManager.CurrentUserFinances) ??
                _incomeScheduleStorageService.CreateNew();

            _incomeScheduleDataModel = _incomeScheduleStorageService.ToDataModel(StorageService, incomeSchedule);

            if (incomeSchedule.Destination?.ID > 0)
                DepositBank = incomeSchedule.Destination;
            else 
                DepositBank = SessionManager.CurrentUserFinances.BankAccounts.First();
        }

        public override Paycheck SaveModel()
        {
            var paycheck = _payCheckStorageService.FromDataModel(StorageService, _dataModel);
            paycheck = _payCheckStorageService.Store(StorageService, paycheck);

            _incomeScheduleDataModel.PaycheckID = paycheck.ID;
            var paySchedule = _incomeScheduleStorageService.FromDataModel(StorageService, _incomeScheduleDataModel);
            paySchedule = _incomeScheduleStorageService.Store(StorageService, paySchedule);

            SessionManager.CurrentUserFinances.Add(paycheck);
            SessionManager.CurrentUserFinances.Add(paySchedule);

            return paycheck;
        }
        public override ValidationResult[] Validate()
        {
            throw new NotImplementedException();
        }
    }
}
