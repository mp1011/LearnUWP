using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Transactions;
using System;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class PaymentScheduleStorageService : SingleModelStorageService<PaymentSchedule, PaymentScheduleDataModel>
    {
        private readonly RecurrenceFactory _recurrenceFactory;

        public PaymentScheduleStorageService(RecurrenceFactory recurrenceFactory)
        {
            _recurrenceFactory = recurrenceFactory;
        }

        public override PaymentSchedule CreateNew()
        {
            return new PaymentSchedule(0,
                new BankAccount(0, string.Empty, 0),
                new GoodOrService(0, string.Empty, 0),
                RecurrenceType.Monthly,
                AmountType.Percent,
                1.0M,
                string.Empty,
                new DateTime(DateTime.Now.Year,DateTime.Now.Month,1),
                DateTime.Now.AddYears(100),
                _recurrenceFactory);
        }

        public override PaymentSchedule FromDataModel(StorageService storageService, PaymentScheduleDataModel dataModel)
        {
            return new PaymentSchedule(
                id: dataModel.ID,
                bankAccount: storageService.LoadModel<BankAccount>(dataModel.BankAccountID),
                expense: storageService.LoadModel<GoodOrService>(dataModel.ExpenseID),
                recurrenceType: (RecurrenceType)dataModel.RecurrenceTypeID,
                amountType: (AmountType)dataModel.AmountTypeID,
                amount: dataModel.Amount,
                description: dataModel.Description,
                firstDate: dataModel.FirstPaymentDate,
                lastDate: dataModel.LastPaymentDate,
                recurrenceFactory: _recurrenceFactory);
        }

        public override PaymentScheduleDataModel ToDataModel(StorageService storageService, PaymentSchedule model)
        {
            return new PaymentScheduleDataModel
            {
                ID = model.ID,
                Amount = model.Amount,
                AmountTypeID = (int)model.AmountType,
                BankAccountID = model.Source.ID,
                ExpenseID = model.Destination.ID,
                Description = model.Description,
                FirstPaymentDate = model.FirstDate,
                LastPaymentDate = model.LastDate,
                RecurrenceTypeID = (int)model.RecurrenceType
            };
        }
    }
}
