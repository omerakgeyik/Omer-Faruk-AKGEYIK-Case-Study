using OpenQA.Selenium;

namespace CaseStudy.Utilities
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(IWebDriver driver, string screenshotName = "")
        {
            var screenshotPath = Path.Combine(ReportHelper.ReportDirectory, "Screenshots");
            Directory.CreateDirectory(screenshotPath);

            var fileName = string.IsNullOrEmpty(screenshotName)
                ? $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                : $"{screenshotName}.png";

            var fullPath = Path.Combine(screenshotPath, fileName);
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(fullPath);
            return fullPath;
        }
    }
}
