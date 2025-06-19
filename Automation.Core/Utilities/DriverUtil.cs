using OpenQA.Selenium;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using Automation.Test.Core.WebDriver;
namespace Automation.Test.Core.Utilities
{
    public class DriverUtil
    {
        public static void GoToUrl(string url)
        {
            BrowserFactory.GetWebDriver().Navigate().GoToUrl(url);
        }

        public static void MaximizeWindow()
        {
            BrowserFactory.GetWebDriver().Manage().Window.Maximize();
        }

        public static string CaptureScreenShot(IWebDriver driver, string className, string testName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var screenshotPath = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationUtils.GetConfigurationByKey("ScreenshotPath"));
            testName = testName.Replace("\"", "");
            var fileName = string.Format(@"Screenshot_{0}_{1}.png", testName, DateTime.Now.ToString("yyyyMMdd_HHmmssff"));
            Directory.CreateDirectory(screenshotPath);
            var fileLocation = string.Format(@"{0}\{1}", screenshotPath, fileName);
            screenshot.SaveAsFile(fileLocation);
            return fileLocation;
        }
        public static Media CaptureScreenShotAndAttachToExtendReport(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();

        }

        public static void CloseBrowser()
        {
            BrowserFactory.GetWebDriver().Quit();
        }
    }
}