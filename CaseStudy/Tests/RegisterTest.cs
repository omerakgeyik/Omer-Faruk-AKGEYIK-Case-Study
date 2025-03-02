using CaseStudy.Pages;
using CaseStudy.Utilities;

namespace CaseStudy.Tests
{
    [TestFixture, Order(1)]
    public class RegisterTest : BaseTest
    {
        private RegisterPage registerPage;

        [SetUp]
        public void SetUp()
        {
            Driver.GetDriver().Navigate().GoToUrl("https://app.forceget.com/system/account/register?requireCloudflareForTesting=false");
            registerPage = new RegisterPage();
        }

        [Test]
        public void SuccessfulRegistrationTest()
        {
            try
            {
                ReportHelper.LogInfo("Starting registration test");
                registerPage.Register();
                ReportHelper.LogInfo("Entered user details");
                Assert.IsTrue(Driver.GetDriver().Url.Contains("forceget"));
                var testEmail = ConfigReader.GetProperty("TestMail");
                var verificationCode = CustomMethods.GetVerificationCode(testEmail);
                registerPage.EnterVerificationCode(verificationCode);
                registerPage.DirectToSignInPage();
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
