using FinancialDucks.Data;
using FinancialDucks.Data.Services;
using FinancialDucks.IOC;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Models.Transactions;
using FinancialDucks.Services;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace FinancialDucks.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        [SetUp]
        public void Setup()
        {
            DataConfig.DataPath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\LearnUWP\Test"; 
                
            OneConfig.Services.FileHelper.ApplicationDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);
            DIRegistrar.RegisterTypes();

            var sqliteFile = IOCContainer.Resolve<SQLiteConnectionProvider>().SQLiteDBFile;
            if (sqliteFile.Exists)
                sqliteFile.Delete();

            FixtureSetup();
        }

        public virtual void FixtureSetup() { }

        [OneTimeTearDown]
        public void Cleanup()
        {
            var storageService = IOCContainer.Resolve<StorageService>();

            var testBanks = storageService
                .LoadModelsForUser<BankAccount>(-1)
                .Where(p => string.IsNullOrEmpty(p.Name) || p.Name.StartsWith("UNIT TEST"))
                .ToArray();

            foreach (var testBank in testBanks)
                storageService.DeleteModelAndDependencies(testBank);

            var testExpenses = storageService
                .LoadModelsForUser<GoodOrService>(-1)
                .Where(p => p.Name.StartsWith("UNIT TEST"))
                .ToArray();

            foreach (var testExpense in testExpenses)
                storageService.DeleteModelAndDependencies(testExpense);

            var testPaychecks = storageService
               .LoadModelsForUser<Paycheck>(-1)
               .Where(p => p.CompanyName.StartsWith("UNIT TEST"))
               .ToArray();

            foreach (var testPaycheck in testPaychecks)
                storageService.DeleteModelAndDependencies(testPaycheck);

            var testSchedules = storageService
                .LoadModelsForUser<PaymentSchedule>(-1)
                .Where(p => p.Description == "UNIT TEST")
                .ToArray();

            foreach(var testSchedule in testSchedules)
                storageService.DeleteModelAndDependencies(testSchedule);

        }
    }
}