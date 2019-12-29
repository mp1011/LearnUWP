using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using FinancialDucks.Services.ModelStorageServices;
using FinancialDucks.Services.UserServices;
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

        //todo
        private BankAccount _depositBank = null;

        public BankAccount DepositBank
        {
            get => _depositBank;
            set
            {
                if (_depositBank != value)
                {
                    _depositBank = value;
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
            IUserSessionManager sessionManager, StorageService storageService, RecurrenceFactory dateService)
            : base(sessionManager,storageService)
        {
            _payCheckStorageService = payCheckStorageService;
            _incomeScheduleStorageService = incomeScheduleStorageService;
        }

        protected override void SetDataModels(Paycheck paycheck)
        {
            _dataModel = _payCheckStorageService.ToDataModel(StorageService, paycheck);

            var incomeSchedule = paycheck.GetIncomeSchedule(SessionManager.CurrentUserFinances);
            _incomeScheduleDataModel = _incomeScheduleStorageService.ToDataModel(StorageService, incomeSchedule);
            DepositBank = incomeSchedule.Destination;
        }

        protected override Paycheck SaveModel()
        {

            //var userFinances = _sessionManager.GetCurrentUserFinances();

            //var startDate = FirstPayDate.DateTime;
            //var paycheck = new Paycheck(
            //    companyName: CompanyName,
            //    initialAmount: Amount);

            //userFinances.AddEntity(paycheck);
            //userFinances.AddTransactionSchedule
            //(
            //    new TransactionSchedule(paycheck, DepositBank,
            //    _dateService.CreateRecurrence(startDate, startDate.AddYears(100), PayCycle))
            //);
            throw new System.NotImplementedException();
        }
    }
}
