using CaseStudy.Utilities;
using NUnit.Framework.Interfaces;

namespace CaseStudy.Tests
{
    [TestFixture]
    public class BaseTest
    {
        [OneTimeSetUp]
        public void SetupSuite()
        {
            ReportHelper.InitializeReport();
        }

        [SetUp]
        public void SetupTest()
        {
            ReportHelper.CreateTest(TestContext.CurrentContext.Test.Name);
            Driver.GetDriver();
        }

        [TearDown]
        public void TeardownTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                ReportHelper.LogFail("Test Failed!");
                ReportHelper.AddScreenshot("Failure Screenshot");
            }
            else
            {
                ReportHelper.LogPass("Test Passed!");
            }

            Driver.QuitDriver();
        }

        [OneTimeTearDown]
        public void TeardownSuite()
        {
            ReportHelper.FlushReport();
        }
    }
}
