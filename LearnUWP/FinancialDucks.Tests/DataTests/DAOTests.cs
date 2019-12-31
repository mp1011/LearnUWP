using FinancialDucks.Data.Models;
using FinancialDucks.Data.Services;
using FinancialDucks.Extensions;
using FinancialDucks.IOC;
using FinancialDucks.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

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

        [TestCase(typeof(BankAccountDataModel), "NAME like 'TEST%'")]
        [TestCase(typeof(PaymentScheduleDataModel), "Description = 'UNIT TEST'")]
        [TestCase(typeof(PaycheckDataModel), "CompanyName = 'TEST paycheck'")]
        public void CanDeleteModels(Type modelType, string where)
        {
            this.DynamicDispatch(nameof(CanDeleteModels), new Type[] { modelType }, new object[] { where });
        }

        public void CanDeleteModels<T>(string where)
        {
            var dao = IOCContainer.Resolve<DAO>();
            var models = dao.Read<T>(where);
            dao.Delete<T>(where);
            var modelsNow = dao.Read<T>(where);
            modelsNow.Length.Should().Be(0);
        }

        [TestCase(typeof(PayCycle), typeof(PayCycleDataModel))]
        [TestCase(typeof(RecurrenceType), typeof(RecurrenceTypeDataModel))]
        [TestCase(typeof(AmountType), typeof(AmountTypeDataModel))]

        public void EnumTablesHaveCorrectValues(Type enumType, Type modelType)
        {
            this.DynamicDispatch(nameof(EnumTablesHaveCorrectValues), new Type[] { enumType, modelType });
        }

        public void EnumTablesHaveCorrectValues<TEnum,TTable>()
            where TEnum: struct
            where TTable : EnumTable
        {
            var dao = IOCContainer.Resolve<DAO>();


            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                var id = (int)(object)value;
                var name = value.ToString();

                var dbValue = dao.Read<TTable>("ID=@id", new { id })
                    .FirstOrDefault();

                dbValue.Should().NotBeNull();
                dbValue.ID.Should().Be(id);
                dbValue.Name.Should().Be(name);
            }
        }
    }
}
