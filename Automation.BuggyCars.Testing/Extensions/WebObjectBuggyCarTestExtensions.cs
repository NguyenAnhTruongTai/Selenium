using OpenQA.Selenium.Support.UI;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Extensions
{
    public static class WebObjectBuggyCarTestExtensions
    {
        public static void SelectDropdownByText(this WebObject webObject, string text)
        {
            var element = webObject.WaitForElementToBeEnabled();
            var select = new SelectElement(element);
            Console.WriteLine($"Selecting '{text}' from dropdown: {webObject.Name}");
            select.SelectByText(text);
        }
    }
}
