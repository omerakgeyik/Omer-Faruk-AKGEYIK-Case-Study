using CaseStudy.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CaseStudy.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly IJavaScriptExecutor js;

        private By signInText = By.TagName("h2");
        private By loginEmail = By.XPath("//*[@placeholder='E-mail']");
        private By loginPassword = By.XPath("//*[@placeholder='Password']");
        private By loginSignInBtn = By.XPath("//span[.=' Sign In ']");
        private By dashboardText = By.XPath("//h4[.=' Dashboard ']");
        public LoginPage()
        {
            this.driver = Driver.GetDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            this.js = (IJavaScriptExecutor)driver;
        }

        public IWebElement SignInText => wait.Until(drv => drv.FindElement(signInText));
        public IWebElement LoginEmail => wait.Until(drv => drv.FindElement(loginEmail));
        public IWebElement LoginPassword => wait.Until(drv => drv.FindElement(loginPassword));
        public IWebElement LoginSignInBtn => wait.Until(drv => drv.FindElement(loginSignInBtn));
        public IWebElement DashboardTxt => wait.Until(drv => drv.FindElement(dashboardText));

        public void Login()
        {
            Assert.IsTrue(SignInText.Displayed);
            LoginEmail.EnterText(ConfigReader.GetProperty("Email"));
            LoginPassword.EnterText(ConfigReader.GetProperty("Password"));
            LoginSignInBtn.ClickElement();
        }

        public void CheckDashboard()
        {
            Assert.IsTrue(DashboardTxt.Displayed);
        }
    }
}
