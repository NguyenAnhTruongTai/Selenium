using OpenQA.Selenium;
using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Pages.ManageAccount
{
    public class RegisterPage : BasePage
    {
        private WebObject _registerButton = new WebObject(By.XPath("//a[text()='Register']"), "Navigate to Register Page Button");
        private WebObject _loginInput = new WebObject(By.Name("login"), "Login Input");
        private WebObject _firstNameInput = new WebObject(By.Name("firstName"), "First Name Input");
        private WebObject _lastNameInput = new WebObject(By.Name("lastName"), "Last Name Input");
        private WebObject _passwordInput = new WebObject(By.Name("password"), "Password Input");
        private WebObject _confirmPasswordInput = new WebObject(By.Name("confirmPassword"), "Confirm Password Input");
        private WebObject _registrationSuccessText = new WebObject(By.XPath("//div[contains(text(), 'Registration is successful')]"), "Registration Success Text");
        private WebObject _registerButtonOfRegisterPage = new WebObject(By.XPath("//button[text()='Register']"), "Register Button");

        public WebObject LoginInput => _loginInput;
        public WebObject FirstNameInput => _firstNameInput;
        public WebObject LastNameInput => _lastNameInput;
        public WebObject PasswordInput => _passwordInput;
        public WebObject ConfirmPasswordInput => _confirmPasswordInput;
        public WebObject RegisterButton => _registerButton;
        public WebObject RegistrationSuccessText => _registrationSuccessText;
        public WebObject RegisterButtonOfRegisterPage => _registerButtonOfRegisterPage;

        public RegisterPage() { }

        public void NavigateToRegisterPage()
        {
            _registerButton.ClickOnElement();
        }

        public void FillRegisterForm(RegisterModel register)
        {
            _loginInput.EnterText(register.login);
            _firstNameInput.EnterText(register.firstName);
            _lastNameInput.EnterText(register.lastName);
            _passwordInput.EnterText(register.password);
            _confirmPasswordInput.EnterText(register.confirmPassword);
        }

        public void ClickOnRegisterButton()
        {
            _registerButtonOfRegisterPage.ClickOnElement();
        }
    }
}