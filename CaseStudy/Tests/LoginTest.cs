using CaseStudy.Pages;
using CaseStudy.Utilities;

namespace CaseStudy.Tests
{
    [TestFixture, Order(2)]
    public class LoginTest : BaseTest
    {
        private LoginPage loginPage;

        [SetUp]
        public void SetUp()
        {
            Driver.GetDriver().Navigate().GoToUrl("https://app.forceget.com/system/account/login");
            loginPage = new LoginPage();
        }

        [Test]
        public void SuccessfulLoginTest()
        {
            try
            {
                ReportHelper.LogInfo("Starting login test");
                loginPage.Login();
                ReportHelper.LogInfo("Entered login details");

                Assert.IsTrue(Driver.GetDriver().Url.Contains("forceget"));

                loginPage.CheckDashboard();
                ReportHelper.LogPass("Verification successful");
            }
            catch (Exception ex)
            {
                ReportHelper.LogFail($"Test failed: {ex.Message}");
                throw;
            }
        }
    }
}
