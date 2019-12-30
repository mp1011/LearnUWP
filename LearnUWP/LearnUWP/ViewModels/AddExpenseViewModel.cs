using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
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
    public class AddExpenseViewModel : FinancialEntityCreateOrUpdateViewModel<GoodOrService>
    {
        private readonly ExpenseStorageService _expenseStorageService;
        private readonly PaymentScheduleStorageService _paymentScheduleStorageService;

        private ExpensesDataModel _dataModel;
        private PaymentScheduleDataModel _payScheduleDataModel;

        public string Description
        {
            get => _dataModel.Description;
            set
            {
                if (_dataModel.Description != value)
                {
                    _dataModel.Description = value;
                    InvokePropertyChange(nameof(Description));
                }
            }
        }

      
        private BankAccount _withdrawalBank;
        public BankAccount WithdrawalBank
        {
            get => _withdrawalBank;
            set
            {
                if (value != null && _withdrawalBank != value)
                {
                    _withdrawalBank = value;
                    _payScheduleDataModel.BankAccountID = value.ID;
                    InvokePropertyChange(nameof(WithdrawalBank));
                }
            }
        }

        public RecurrenceType RecurrenceType
        {
            get => (RecurrenceType)_payScheduleDataModel.RecurrenceTypeID;
            set
            {
                if(_payScheduleDataModel.RecurrenceTypeID != (int)value)
                {
                    _payScheduleDataModel.RecurrenceTypeID = (int)value;
                    InvokePropertyChange(nameof(RecurrenceType));
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

        public DateTimeOffset FirstPayDate
        {
            get => _payScheduleDataModel.FirstPaymentDate;
            set
            {
                if (_payScheduleDataModel.FirstPaymentDate != value.DateTime)
                {
                    _payScheduleDataModel.FirstPaymentDate = value.DateTime;
                    InvokePropertyChange(nameof(FirstPayDate));
                }
            }
        }

        public DateTimeOffset LastPayDate
        {
            get => _payScheduleDataModel.FirstPaymentDate;
            set
            {
                if (_payScheduleDataModel.LastPaymentDate != value.DateTime)
                {
                    _payScheduleDataModel.LastPaymentDate = value.DateTime;
                    InvokePropertyChange(nameof(LastPayDate));
                }
            }
        }

        //todo, create a control for this
        public AmountType AmountType
        {
            get => (AmountType)_payScheduleDataModel.AmountTypeID;
            set
            {
                if(_payScheduleDataModel.AmountTypeID != (int)value)
                {
                    _payScheduleDataModel.AmountTypeID = (int)value;
                    InvokePropertyChange(nameof(AmountType));
                }
            }
        }

        public AddExpenseViewModel(IUserSessionManager sessionManager, StorageService storageService, RecurrenceFactory dateService,
            ExpenseStorageService expenseStorageService, PaymentScheduleStorageService paymentScheduleStorageService) 
            :base(sessionManager,storageService)
        {
            _expenseStorageService = expenseStorageService;
            _paymentScheduleStorageService = paymentScheduleStorageService;
            
        }
        protected override void SetDataModels(GoodOrService model)
        {
            _dataModel = _expenseStorageService.ToDataModel(StorageService, model);

            var paymentSchedule = model.GetPaymentSchedule(SessionManager.CurrentUserFinances)
                ?? _paymentScheduleStorageService.CreateNew();

            _payScheduleDataModel = _paymentScheduleStorageService.ToDataModel(StorageService, paymentSchedule);

            WithdrawalBank = paymentSchedule.Source ?? SessionManager.CurrentUserFinances.BankAccounts.First();
        }

        public override GoodOrService SaveModel()
        {
            var expense = _expenseStorageService.FromDataModel(StorageService, _dataModel);
            expense = _expenseStorageService.Store(StorageService, expense);

            _payScheduleDataModel.ExpenseID = expense.ID;
            var paySchedule = _paymentScheduleStorageService.FromDataModel(StorageService, _payScheduleDataModel);
            paySchedule= _paymentScheduleStorageService.Store(StorageService, paySchedule);

            SessionManager.CurrentUserFinances.Add(expense);
            SessionManager.CurrentUserFinances.Add(paySchedule);

            return expense;
        }
    }
}
