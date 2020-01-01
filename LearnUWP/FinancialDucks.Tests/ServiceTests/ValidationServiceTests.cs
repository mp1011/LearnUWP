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
        [TestCase(null,"Name can not be blank")]
        [TestCase("", "Name can not be blank")]
        [TestCase("something", null)]

        public void CanValidateBankAccountName(string name, string expectedMessage)
        {
            var validationService = IOCContainer.Resolve<ValidationService>();

            var bank = new BankAccountDataModel { Name = name };

            var result = validationService.ValidateModel(bank)
                .Where(v => v.PropertyName == nameof(BankAccountDataModel.Name))
                .SingleOrDefault();

            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }

        [TestCase(null, "CompanyName can not be blank")]
        [TestCase("", "CompanyName can not be blank")]
        [TestCase("something", null)]

        public void CanValidatePaycheckCompanyName(string name, string expectedMessage)
        {
            var validationService = IOCContainer.Resolve<ValidationService>();

            var bank = new PaycheckDataModel { CompanyName = name };

            var result = validationService.ValidateModel(bank)
                .Where(v => v.PropertyName == nameof(PaycheckDataModel.CompanyName))
                .SingleOrDefault();

            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
        }


        [TestCase(0, "InitialAmount must be greater than $0")]
        [TestCase(-10, "InitialAmount must be greater than $0")]
        [TestCase(0.01, null)]

        public void CanValidatePaycheckAmount(decimal amount, string expectedMessage)
        {
            var validationService = IOCContainer.Resolve<ValidationService>();

            var bank = new PaycheckDataModel { CompanyName = "anything", InitialAmount = amount };

            var result = validationService.ValidateModel(bank)
                .Where(v => v.PropertyName == nameof(PaycheckDataModel.InitialAmount))
                .SingleOrDefault();

            result.Should().NotBeNull();
            result.Message.Should().Be(expectedMessage);
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
