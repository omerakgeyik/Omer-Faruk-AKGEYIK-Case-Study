using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace CaseStudy.Utilities
{
    public class Driver
    {
        private Driver() { }

        private static IWebDriver _driver;

        public static IWebDriver GetDriver()
        {
            if (_driver == null)
            {
                string browser = ConfigReader.GetProperty("browser");

                switch (browser.ToLower())
                {
                    case "firefox":
                        _driver = new FirefoxDriver();
                        break;
                    case "safari":
                        _driver = new SafariDriver();
                        break;
                    case "edge":
                        _driver = new EdgeDriver();
                        break;
                    default:
                        var options = new ChromeOptions();
                        options.AddExcludedArgument("enable-automation");
                        //options.AddArgument("--headless"); 
                        options.AddArgument("--no-sandbox");
                        options.AddArgument("--disable-dev-shm-usage");
                        options.AddArgument("--disable-blink-features=AutomationControlled");
                        options.AddAdditionalOption("useAutomationExtension", false);
                        _driver = new ChromeDriver(options);
                        break;
                }

                _driver.Manage().Window.Maximize();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }

            return _driver;
        }

        public static void QuitDriver()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}
