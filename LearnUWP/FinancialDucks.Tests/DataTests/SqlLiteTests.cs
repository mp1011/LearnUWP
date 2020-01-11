using Dapper;
using Dapper.Contrib.Extensions;
using FinancialDucks.Data.Models;
using FinancialDucks.Data.Services;
using FinancialDucks.IOC;
using FinancialDucks.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialDucks.Tests.DataTests
{
    public class SqLiteTests :TestBase
    {
        [TestCase]
        public void CanAccessTableWithSqlLite()
        {
  
            var bank = new BankAccountDataModel();
            bank.Name = "UNIT TEST " + Guid.NewGuid();

            var dao = IOCContainer.Resolve<DAO>();
            dao.Upsert(bank);

            bank.ID.Should().NotBe(0);

            var loadedBank = dao.Read<BankAccountDataModel>("ID=@ID", new { bank.ID })
                .FirstOrDefault();

            loadedBank.Should().NotBeNull();
            loadedBank.Name.Should().Be(bank.Name);

            var bank2 = new BankAccountDataModel();
            bank2.Name = "UNIT TEST " + Guid.NewGuid();
            dao.Upsert(bank2);

            bank2.LocalID.Should().Be(2);

        }
    }
}
