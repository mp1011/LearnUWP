﻿using FinancialDucks.Data.Models;
using FinancialDucks.Models;
using FinancialDucks.Models.Transactions;
using System;

namespace FinancialDucks.Services.ModelStorageServices
{
    public class IncomeScheduleStorageService : SingleModelStorageService<IncomeSchedule, IncomeScheduleDataModel>
    {
        private readonly RecurrenceFactory _recurrenceFactory;

        public IncomeScheduleStorageService(RecurrenceFactory recurrenceFactory)
        {
            _recurrenceFactory = recurrenceFactory;
        }

        public override IncomeSchedule CreateNew()
        {
            return new IncomeSchedule(
                   id: 0,
                   paycheck: new Paycheck(0, string.Empty, 0),
                   bankAccount: new BankAccount(0, string.Empty, 0),
                   payCycle: PayCycle.FirstOfTheMonth,
                   firstDate: DateTime.Now,
                   lastDate: DateTime.Now.AddYears(100),
                   recurrenceFactory: _recurrenceFactory);
        }                   

        public override IncomeSchedule FromDataModel(StorageService storageService, IncomeScheduleDataModel dataModel)
        {
            return new IncomeSchedule(
                id: dataModel.ID,
                paycheck: storageService.LoadModel<Paycheck>(dataModel.PaycheckID),
                bankAccount: storageService.LoadModel<BankAccount>(dataModel.DepositBankID),
                payCycle: (PayCycle)dataModel.PayCycleID,
                firstDate: dataModel.FirstPaymentDate,
                lastDate:dataModel.LastPaymentDate ?? dataModel.FirstPaymentDate.AddYears(1000),
                recurrenceFactory: _recurrenceFactory);
        }

        public override IncomeScheduleDataModel ToDataModel(StorageService storageService, IncomeSchedule model)
        {
            return new IncomeScheduleDataModel
            {
                DepositBankID = model.Destination.ID,
                PaycheckID = model.Source.ID,
                ID = model.ID,
                PayCycleID = (int)model.PayCycle,
                FirstPaymentDate = model.FirstDate,
                LastPaymentDate = model.LastDate
            };
        }

        public override void DeleteModelAndDependencies(StorageService storageService, IncomeSchedule model)
        {
            storageService.DAO.Delete<IncomeScheduleDataModel>("ID=@ID", new { model.ID });
        }
    }
}
