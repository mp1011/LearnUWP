using FinancialDucks.Data.Models;
using FinancialDucks.Data.Services;
using FinancialDucks.IOC;
using FinancialDucks.Models;
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
        public void CanCreateAndUpdateBankAccountFromDatabase()
        {
            var storageService = IOCContainer.Resolve<StorageService>();
            var dao = IOCContainer.Resolve<DAO>();

            var newBankAccount = new BankAccount(
                name: "TEST" + Guid.NewGuid().ToString(),
                initialAmount: 1234.56M);

            var modelInDatabase = dao.Read<BankAccountDataModel>("Name=@Name", new { newBankAccount.Name }).SingleOrDefault();
            modelInDatabase.Should().BeNull();

            storageService.StoreModel(newBankAccount);

            modelInDatabase = dao.Read<BankAccountDataModel>("Name=@Name", new { newBankAccount.Name }).SingleOrDefault();
            modelInDatabase.Should().NotBeNull();
            modelInDatabase.Name.Should().Be(newBankAccount.Name);

            var dataModel = newBankAccount.ToDataModel();
            dataModel.InitialAmount = 543.21M;
            newBankAccount.SetFrom(dataModel);

            storageService.StoreModel(newBankAccount);
            modelInDatabase = dao.Read<BankAccountDataModel>("Name=@Name", new { newBankAccount.Name }).SingleOrDefault();
            modelInDatabase.Should().NotBeNull();
            modelInDatabase.InitialAmount.Should().Be(dataModel.InitialAmount);
        }
    }
}
