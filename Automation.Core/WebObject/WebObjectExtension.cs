using Automation.Core.Drivers;
using Automation.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Automation.Core.WebObject
{
    public static class WebObjectExtension
    {
        static int timeout = int.Parse(ConfigurationUtils.GetSectionValue("WebDriver", "WaitTimeout"));

        public static IWebElement WaitForElementtoBeVisible(this WebObject webObject)
        {
            var Wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(timeout));
            Wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return Wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
        }

        public static IWebElement WaitForElementtoBeClickable(this WebObject webObject)
        {
            var Wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(timeout));
            Wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return Wait.Until(ExpectedConditions.ElementToBeClickable(webObject.By));
        }
        public static IWebElement WaitForElementToBeEnabled(this WebObject webObject)
        {
            var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            return wait.Until(driver =>
            {
                var element = driver.FindElement(webObject.By);
                return (element.Displayed && element.Enabled) ? element : null;
            });
        }

        public static bool IsElementDisplayed(this WebObject webObject)
        {
            var Wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(timeout));
            Wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            return Wait.Until(ExpectedConditions.ElementIsVisible(webObject.By)).Displayed;
        }

        public static void ClickOnElement(this WebObject webObject)
        {
            var driver = BrowserFactory.GetWebDriver();
            var element = webObject.WaitForElementtoBeClickable();

            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);

            Console.WriteLine($"Clicking on: {webObject.Name}");
            element.Click();
        }

        public static void EnterText(this WebObject webObject, string text)
        {
            var element = WaitForElementtoBeVisible(webObject);
            Console.WriteLine($"Clicking on: {text} to element: " + webObject.Name);
            element.SendKeys(text);
        }
        public static void PressEnter(this WebObject webObject, string text)
        {
            var element = WaitForElementtoBeVisible(webObject);
            Console.WriteLine($"Entering '{text}' and pressing Enter on element: {webObject.Name}");
            element.SendKeys(text + Keys.Enter);
        }
        public static string GetTextFromElement(this WebObject webObject)
        {
            var element = WaitForElementtoBeVisible(webObject);
            return element.Text;
        }
    }
}