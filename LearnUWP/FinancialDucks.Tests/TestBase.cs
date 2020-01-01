using FinancialDucks.IOC;
using FinancialDucks.Models;
using FinancialDucks.Models.FinancialEntities;
using FinancialDucks.Services;
using NUnit.Framework;
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
            OneConfig.Services.FileHelper.ApplicationDirectory = new DirectoryInfo(TestContext.CurrentContext.TestDirectory);
            DIRegistrar.RegisterTypes();

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
        }
    }
}