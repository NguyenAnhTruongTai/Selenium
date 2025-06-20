using OpenQA.Selenium;
using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Pages.ManageAccount
{
    public class LoginPage : BasePage
    {
        private WebObject _loginInput = new WebObject(By.Name("login"), "Login Input");
        private WebObject _passwordInput = new WebObject(By.Name("password"), "Password Input");
        private WebObject _loginButton = new WebObject(By.XPath("//button[text()='Login']"), "Login Button");
        private WebObject _profileButton = new WebObject(By.LinkText("Profile"), "Profile Button");
        private WebObject _logoutButton = new WebObject(By.LinkText("Logout"), "Logout Button");
        private WebObject _greetingText = new WebObject(By.XPath("//span[contains(@class, 'nav-link') and contains(text(), 'Hi,')]"), "Greeting Text");

        public WebObject LoginInput => _loginInput;
        public WebObject PasswordInput => _passwordInput;
        public WebObject LoginButton => _loginButton;
        public WebObject ProfileButton => _profileButton;
        public WebObject LogoutButton => _logoutButton;
        public WebObject GreetingText => _greetingText;

        public LoginPage() { }

        public void FillLoginForm(LoginModel login)
        {
            _loginInput.EnterText(login.login);
            _passwordInput.EnterText(login.password);
            _loginButton.ClickOnElement();
        }
    }
}