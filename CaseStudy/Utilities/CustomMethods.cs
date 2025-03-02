using Mailosaur;
using Mailosaur.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CaseStudy.Utilities
{
    public static class CustomMethods
    {
        public static void ClickElement(this IWebElement webElement)
        {
            webElement.Click();
        }

        public static void EnterText(this IWebElement webElement, string text)
        {
            webElement.Clear();
            webElement.SendKeys(text);
        }

        public static void SelectCustom(this IWebElement element, By optionsLocator, string targetText)
        {
            var driver = Driver.GetDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var js = (IJavaScriptExecutor)driver;

            int maxScrollAttempts = int.Parse(ConfigReader.GetProperty("maxScrollAttempts"));
            int scrollPixelAmount = int.Parse(ConfigReader.GetProperty("scrollPixelAmount"));

            int scrollAttempts = 0;

            try
            {
                element.Click();

                var scrollContainer = wait.Until(drv =>
                    drv.FindElement(By.TagName("cdk-virtual-scroll-viewport"))
                );

                HashSet<string> checkedOptions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                while (scrollAttempts < maxScrollAttempts)
                {
                    var options = driver.FindElements(optionsLocator);

                    foreach (var option in options)
                    {
                        try
                        {
                            string optionText = option.Text.Trim();

                            if (checkedOptions.Contains(optionText)) continue;

                            checkedOptions.Add(optionText);

                            if (optionText.Equals(targetText, StringComparison.OrdinalIgnoreCase))
                            {
                                option.Click();
                                return;
                            }
                        }
                        catch (StaleElementReferenceException)
                        {
                            break;
                        }
                    }

                    js.ExecuteScript($"arguments[0].scrollTop += {scrollPixelAmount};", scrollContainer);
                    scrollAttempts++;
                    Thread.Sleep(500);
                }
            }
            catch
            {
                // Error
            }
        }

        public static string GetVerificationCode(string sentToEmail)
        {
            var client = new MailosaurClient(ConfigReader.GetProperty("APIKey"));

            var criteria = new SearchCriteria
            {
                SentTo = sentToEmail

            };

            var messages = client.Messages.Get(ConfigReader.GetProperty("ServerId"), criteria, timeout: 10_000);
            var match = messages.Html.Codes[0];

            return match.Value;
        }

        public static void EnterOtpCode(this IWebDriver driver, string otpCode)
        {
            driver = Driver.GetDriver();
            if (otpCode.Length != 6)
                throw new ArgumentException("OTP code must be 6 digits");

            var queue = new Queue<char>(otpCode.ToCharArray());

            for (int i = 1; i <= 6; i++)
            {
                var locator = By.XPath($"//*[@formarrayname='otp']/input[{i}]");
                var element = driver.WaitForElement(locator);

                element.Click();
                element.Clear();
                element.SendKeys(queue.Dequeue().ToString());
            }
        }

        public static IWebElement WaitForElement(this IWebDriver driver, By locator, int timeoutInSeconds = 10)
        {
            driver = Driver.GetDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(drv =>
            {
                var element = drv.FindElement(locator);
                return (element.Displayed && element.Enabled) ? element : null;
            });
        }
    }
}
