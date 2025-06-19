using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Jira011.Core
{
    public class BrowserFactory
    {
        public static ThreadLocal<IWebDriver> _threadLocalWebDriver = new ThreadLocal<IWebDriver>();
        public static void InitializeDriver(string browserName)
        {
            switch (browserName)
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArguments("test-type");
                    chromeOptions.AddArguments("--no-sandbox");

                    _threadLocalWebDriver.Value = new ChromeDriver(chromeOptions);
                    break;

                case "firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArguments("test-type");
                    firefoxOptions.AddArguments("--no-sandbox");

                    _threadLocalWebDriver.Value = new FirefoxDriver(firefoxOptions);
                    break;

                case "edge":
                    new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.MatchingBrowser);
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArguments("test-type");
                    edgeOptions.AddArguments("--no-sandbox");

                    _threadLocalWebDriver.Value = new EdgeDriver(edgeOptions);
                    break;

                default:
                    throw new ArgumentException($"Browser '{browserName}' is not supported. Please use 'chrome', 'firefox', or 'edge'.");
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
            _threadLocalWebDriver.Value.Quit();
            _threadLocalWebDriver.Value.Dispose();
        }
    }
}