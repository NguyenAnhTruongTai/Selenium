using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Automation.Core.Drivers
{
    public class BrowserFactory
    {
        public static ThreadLocal<IWebDriver?> _threadLocalWebDriver = new ThreadLocal<IWebDriver?>();
        public static void CreateBrowser(string browserName)
        {
            switch (browserName)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("test-type", "--no-sandbox");
                    _threadLocalWebDriver.Value = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("test-type", "--no-sandbox");
                    _threadLocalWebDriver.Value = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("test-type", "--no-sandbox");
                    _threadLocalWebDriver.Value = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new ArgumentException($"Browser '{browserName}' is not supported.");
            }
        }


        public static IWebDriver GetWebDriver()
        {
            if (_threadLocalWebDriver.Value == null)
            {
                throw new InvalidOperationException("WebDriver has not been initialized. Please initialize it before accessing.");
            }
            return _threadLocalWebDriver.Value;
        }

        public static void CleanUpWebDriver()
        {
            if (_threadLocalWebDriver.Value != null)
            {
                _threadLocalWebDriver.Value.Quit();
                _threadLocalWebDriver.Value.Dispose();
                _threadLocalWebDriver.Value = null;
            }
        }
    }
}