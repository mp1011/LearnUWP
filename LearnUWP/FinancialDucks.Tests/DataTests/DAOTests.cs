using FinancialDucks.Data.Models;
using FinancialDucks.Data.Services;
using FinancialDucks.IOC;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace FinancialDucks.Tests.DataTests
{
    public class DAOTests : TestBase
    {
        [Test]
        public void ConnectionStringIsFullyResolved()
        {
            var connectionProvider = IOCContainer.Resolve<IConnectionProvider>();
            using (var conn = connectionProvider.CreateConnection())
            {
                conn.ConnectionString
                    .Should()
                    .NotBeEmpty();
                    
                conn.ConnectionString
                    .Contains("#")
                    .Should()
                    .BeFalse();
            }
        }

        [Test]
        public void CanReadFromDatabase()
        {
            var dao = IOCContainer.Resolve<DAO>();
            var bankAccounts = dao.Read<BankAccountDataModel>();
            bankAccounts.Length
                .Should()
                .BeGreaterThan(0);
        }
    }
}
