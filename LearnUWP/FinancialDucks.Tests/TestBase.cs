using FinancialDucks.IOC;
using NUnit.Framework;
using System.IO;

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
    }
}