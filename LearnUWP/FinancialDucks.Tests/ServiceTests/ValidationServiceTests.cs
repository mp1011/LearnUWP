using FinancialDucks.Data.Models;
using FinancialDucks.IOC;
using FinancialDucks.Models;
using FinancialDucks.Services;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace FinancialDucks.Tests.ServiceTests
{
    public class ValidationServiceTests : TestBase
    {
        [Test]
        public void BankAccountNameCantBeEmpty()
        {
            var validationService = IOCContainer.Resolve<ValidationService>();

            var bank = new BankAccountDataModel();

            var result = validationService.ValidateModel(bank)
                .Where(v => !v.IsValid)
                .SingleOrDefault();

            result.Should().NotBeNull();
            result.Message.Should().Be("Name can not be blank");

            bank.Name = "anything";
            result = validationService.ValidateModel(bank)
               .Where(v => !v.IsValid)
               .SingleOrDefault();

            result.Should().BeNull();
        }

        [Test]
        public void CannotSaveBankAccountWithEmptyName()
        {
            var storageService = IOCContainer.Resolve<StorageService>();

            var badBank = new BankAccount(0, null, 0);

            storageService.Invoking(
                s => s.StoreModel(badBank))
                .Should()
                .Throw<Exception>()
                .WithMessage("Name can not be blank");
        }
    }
}
