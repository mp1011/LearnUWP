using FinancialDucks.Data.Models;
using FinancialDucks.Data.Services;
using FinancialDucks.IOC;
using FinancialDucks.Models;
using FinancialDucks.Models.UserData;
using FinancialDucks.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Tests.ServiceTests
{
    public class StorageServiceTests : TestBase
    {
        [Test]
        public void CanLoadUserFinancesForCurrentUser()
        {
            var storageService = IOCContainer.Resolve<StorageService>();
            var userFinances = storageService.LoadModelsForUser<UserFinances>(-1)
                .FirstOrDefault();

            userFinances.Should().NotBeNull();
            userFinances.BankAccounts.Should().NotBeEmpty();
            userFinances.Paychecks.Should().NotBeEmpty();
        }

        [Test]
        public void CanCreateAndUpdateBankAccountFromDatabase()
        {
            var storageService = IOCContainer.Resolve<StorageService>();
            var dao = IOCContainer.Resolve<DAO>();

            var newBankAccount = new BankAccount(
                id:0,
                name: "TEST" + Guid.NewGuid().ToString(),
                initialAmount: 1234.56M);

            var modelInDatabase = dao.Read<BankAccountDataModel>("Name=@Name", new { newBankAccount.Name }).SingleOrDefault();
            modelInDatabase.Should().BeNull();

            newBankAccount = storageService.StoreModel(newBankAccount);
            newBankAccount.ID.Should().BeGreaterThan(0);

            modelInDatabase = dao.Read<BankAccountDataModel>("Name=@Name", new { newBankAccount.Name }).SingleOrDefault();
            modelInDatabase.Should().NotBeNull();
            modelInDatabase.Name.Should().Be(newBankAccount.Name);

            newBankAccount = new BankAccount(modelInDatabase.ID, modelInDatabase.Name, 543.21M);
            storageService.StoreModel(newBankAccount);

            modelInDatabase = dao.Read<BankAccountDataModel>("Name=@Name", new { newBankAccount.Name }).SingleOrDefault();
            modelInDatabase.Should().NotBeNull();
            modelInDatabase.InitialAmount.Should().Be(newBankAccount.InitialAmount);
        }

        [Test]
        public void CanLoadBankAccounts()
        {
            var storageService = IOCContainer.Resolve<StorageService>();
            var dao = IOCContainer.Resolve<DAO>();

            var banksInDB = dao.Read<BankAccountDataModel>();
            var bankModels = storageService.LoadModelsForUser<BankAccount>(-1);

            bankModels.Length.Should().BeGreaterThan(0);
            bankModels.Length.Should().Be(banksInDB.Length);
            bankModels.Max(p => p.ID).Should().Be(banksInDB.Max(p => p.ID));
        }

    }
}
