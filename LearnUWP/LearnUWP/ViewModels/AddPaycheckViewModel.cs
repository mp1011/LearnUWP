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

        private PaycheckDataModel _dataModel;
        private IncomeScheduleDataModel _schedule;

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
            get => (PayCycle)_schedule.PayCycleID;
            set
            {
                if (_schedule.PayCycleID != (int)value)
                {
                    _schedule.PayCycleID = (int)value;
                    InvokePropertyChange(nameof(PayCycle));
                }
            }
        }

        public DateTimeOffset FirstPayDate
        {
            get => _schedule.PaymentDate;
            set
            {
                if (_schedule.PaymentDate != value.DateTime)
                {
                    _schedule.PaymentDate = value.DateTime;
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

        public AddPaycheckViewModel(PayCheckStorageService payCheckStorageService, IUserSessionManager sessionManager, StorageService storageService, DateService dateService)
            : base(sessionManager,storageService)
        {
            _payCheckStorageService = payCheckStorageService;
        }

        protected override void SetDataModels(StorageService storageService, Paycheck model)
        {
            _dataModel = _payCheckStorageService.ToDataModel(model);

            throw new NotImplementedException("load schedule");
            //todo
            //DepositBank = sessionManager
            //    .CurrentUserFinances
            //    .BankAccounts
            //    .First();
        }

        protected override Paycheck CreateOrUpdate(StorageService storageService)
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
