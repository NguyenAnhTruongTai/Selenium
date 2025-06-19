using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Jira011.Utils;
using System.Globalization;


namespace Jira011.Core
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
        //used for <select>
        public static void SelectByText(this WebObject webObject, string text)
        {
            var element = webObject.WaitForElementtoBeVisible();
            var selectElement = new SelectElement(element);
            foreach (var opt in selectElement.Options)
            {
                if (opt.Text.Trim().ToLower().Contains(text.Trim().ToLower()))
                {
                    opt.Click();
                    return;
                }
            }
            selectElement.SelectByText(text.Trim());
        }
        public static string GetTextFromElement(this WebObject webObject)
        {
            var element = WaitForElementtoBeVisible(webObject);
            return element.Text;
        }
        //namePrefix for debug
        public static WebObject CreateLabelOptionForClick(string labelText)
        {
            return new WebObject(By.XPath($"//label[text()='{labelText}']"));
        }
        //used for select <div>
        public static WebObject CreateDropdownOptionForClick(string optionText)
        {
            return new WebObject(By.XPath($"//div[contains(@class, '-menu')]//div[normalize-space(text())='{optionText}']"));
        }
        public static string ConvertDobFormat(string dob)
        {
            var date = DateTime.ParseExact(dob, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            return date.ToString("dd MMMM,yyyy", CultureInfo.InvariantCulture);
        }
        public static IReadOnlyCollection<IWebElement> FindElements(this WebObject webObject)
        {
            var driver = BrowserFactory.GetWebDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(drv => drv.FindElements(webObject.By).Count > 0);
            return driver.FindElements(webObject.By);
        }
    }
}