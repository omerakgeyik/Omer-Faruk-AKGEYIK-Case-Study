using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace CaseStudy.Utilities
{
    public static class ReportHelper
    {
        private static ExtentReports _extent;
        private static ExtentTest _test;
        private static string _reportPath;

        public static string ReportDirectory => _reportPath;

        public static void InitializeReport()
        {
            _reportPath = Path.Combine(
                "C:\\Users\\omerf\\source\\repos\\CaseStudy\\CaseStudy\\",
                "TestReports",
                DateTime.Now.ToString("yyyyMMdd_HHmmss")
            );

            Directory.CreateDirectory(_reportPath);

            var htmlReporter = new ExtentHtmlReporter(Path.Combine(_reportPath, "Index.html"));
            htmlReporter.Config.DocumentTitle = "Automation Test Report";
            htmlReporter.Config.ReportName = "Regression Suite";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);

            _extent.AddSystemInfo("Environment", "QA");
            _extent.AddSystemInfo("Browser", "Chrome");
            _extent.AddSystemInfo("OS", "Windows 10");
        }

        public static void CreateTest(string testName)
        {
            _test = _extent.CreateTest(testName);
        }

        public static void LogPass(string message)
        {
            _test.Pass(message);
        }

        public static void LogFail(string message)
        {
            _test.Fail(message);
        }

        public static void LogInfo(string message)
        {
            _test.Info(message);
        }

        public static void AddScreenshot(string caption)
        {
            try
            {
                var screenshotPath = ScreenshotHelper.CaptureScreenshot(Driver.GetDriver());
                _test.AddScreenCaptureFromPath(screenshotPath, caption);
            }
            catch (Exception ex)
            {
                _test.Warning($"Failed to capture screenshot: {ex.Message}");
            }
        }

        public static void FlushReport()
        {
            _extent.Flush();
        }
    }
}
