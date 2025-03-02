using CaseStudy.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace CaseStudy.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly IJavaScriptExecutor js;

        private By firstName = By.Id("firstName");
        private By lastName = By.Id("lastName");
        private By countryInput = By.XPath("//forceget-country-dropdown//input");
        private By countryListItems = By.XPath("//*[@class='cdk-overlay-pane']//span[@class='truncate']");
        private By mobileNumber = By.Id("phoneNumber");
        private By companyName = By.Id("companyName");
        private By emailAdress = By.XPath("//*[@type='email']");
        private By titleInput = By.XPath("//*[@formcontrolname='jobTitle']");
        private By titleListItems = By.ClassName("ant-select-item-option-content");
        private By password = By.XPath("//*[@formcontrolname='password']");
        private By confirmPassword = By.XPath("//*[@formcontrolname='passwordConfirm']");
        private By policyCheckBox = By.ClassName("checkbox-box");
        private By acceptBtn = By.XPath("//span[.=' Accept ']");
        private By signUpBtn = By.XPath("//span[.=' Agree & Sign-Up ']");
        private By signInBtn = By.XPath("//*[.='Sign In']");
        private By OTPPopup = By.XPath("//h1[.='Please check your email']");

        private By errorReturn = By.XPath("//*[@class='cdk-overlay-container']//nz-notification-container/div[2]/nz-notification/div/div/div/div/div[1]/div");



        public RegisterPage()
        {
            this.driver = Driver.GetDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            this.js = (IJavaScriptExecutor)driver;
        }

        public IWebElement FirstName => wait.Until(drv => drv.FindElement(firstName));
        public IWebElement LastName => wait.Until(drv => drv.FindElement(lastName));
        public IWebElement MobileNumber => wait.Until(drv => drv.FindElement(mobileNumber));
        public IWebElement CompanyName => wait.Until(drv => drv.FindElement(companyName));
        public IWebElement Email => wait.Until(drv => drv.FindElement(emailAdress));
        public IWebElement Password => wait.Until(drv => drv.FindElement(password));
        public IWebElement ConfrimPassword => wait.Until(drv => drv.FindElement(confirmPassword));
        public IWebElement PolicyCheckBox => wait.Until(drv => drv.FindElement(policyCheckBox));
        public IWebElement AcceptBtn => wait.Until(drv => drv.FindElement(acceptBtn));
        public IWebElement SignUpBtn => wait.Until(drv => drv.FindElement(signUpBtn));
        public IWebElement SignInBtn => wait.Until(drv => drv.FindElement(signInBtn));
        public IWebElement InvalidCodeError => wait.Until(drv => drv.FindElement(errorReturn));
        public IWebElement OTPPopUp => wait.Until(drv => drv.FindElement(OTPPopup));



        public void Register()
        {
            FirstName.EnterText(ConfigReader.GetProperty("FirstName"));
            LastName.EnterText(ConfigReader.GetProperty("LastName"));
            SelectCountry(ConfigReader.GetProperty("Country"));
            MobileNumber.EnterText(ConfigReader.GetProperty("MobileNumber"));
            CompanyName.EnterText(ConfigReader.GetProperty("CompanyName"));
            Email.EnterText(ConfigReader.GetProperty("Email"));
            SelectJobTitle(ConfigReader.GetProperty("Title"));
            Password.EnterText(ConfigReader.GetProperty("Password"));
            ConfrimPassword.EnterText(ConfigReader.GetProperty("ConfirmPassword"));
            PolicyCheckBox.ClickElement();
            Thread.Sleep(2000);
            js.ExecuteScript("arguments[0].scrollIntoView({block: 'end'});", AcceptBtn);
            AcceptBtn.ClickElement();
            Thread.Sleep(20000);
            SignUpBtn.ClickElement();
            Thread.Sleep(6000);
            Assert.IsTrue(OTPPopUp.Displayed);


        }

        public void SelectCountry(string countryName)
        {
            var countryInputElement = wait.Until(drv => drv.FindElement(countryInput));
            countryInputElement.SelectCustom(
                countryListItems,
                countryName
            );
        }

        public void SelectJobTitle(string titleName)
        {
            var titleInputElement = wait.Until(drv => drv.FindElement(titleInput));
            titleInputElement.ClickElement();

            var jobTitles = driver.FindElements(titleListItems);

            foreach (var jobTitle in jobTitles)
            {
                if (jobTitle.Text.Equals(ConfigReader.GetProperty("Title")))
                {
                    jobTitle.ClickElement();
                    break;
                }
            }


        }

        public void DirectToSignInPage()
        {
            wait.Until(d => d.Url == "https://app.forceget.com/system/account/login");
            Assert.AreEqual("https://app.forceget.com/system/account/login", driver.Url);
        }

        public void EnterVerificationCode(string code)
        {
            driver.EnterOtpCode(code);
        }
    }
}
