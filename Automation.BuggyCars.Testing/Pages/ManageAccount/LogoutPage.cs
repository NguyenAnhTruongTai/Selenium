using Automation.Core.WebObject;
using OpenQA.Selenium;

namespace Automation.BuggyCars.Testing.Pages.ManageAccount
{
    public class LogoutPage : BasePage
    {
        private WebObject _logoutButton = new WebObject(By.XPath("//a[contains(text(), 'Logout')]"), "Logout Button");


        public WebObject LogoutButton => _logoutButton;
        public LogoutPage() { }


        public void ClickOnLogoutButton()
        {
            _logoutButton.ClickOnElement();
        }
    }
}