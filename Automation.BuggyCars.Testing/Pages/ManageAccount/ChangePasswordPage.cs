using OpenQA.Selenium;
using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Pages.ManageAccount
{
    public class ChangePasswordPage : BasePage
    {
        private WebObject _profileButton = new WebObject(By.XPath("//a[text()='Profile']"), "Navigate to Profile Page Button");

        private WebObject _currentPasswordInput = new WebObject(By.Id("currentPassword"), "Current Password Input");
        private WebObject _newPasswordInput = new WebObject(By.Id("newPassword"), "New Password Input");
        private WebObject _confirmNewPasswordInput = new WebObject(By.Id("confirmNewPassword"), "Confirm New Password Input");
        private WebObject _saveButton = new WebObject(By.XPath("//button[text()='Save']"), "Save Button");

        public WebObject CurrentPasswordInput => _currentPasswordInput;
        public WebObject NewPasswordInput => _newPasswordInput;
        public WebObject ConfirmNewPasswordInput => _confirmNewPasswordInput;
        public WebObject SaveButton => _saveButton;

        public ChangePasswordPage() { }

        public void NavigateToProfilePage()
        {
            _profileButton.ClickOnElement();
        }

        public void FillChangePasswordForm(ChangePasswordModel changePassword)
        {
            _currentPasswordInput.EnterText(changePassword.currentPassword);
            _newPasswordInput.EnterText(changePassword.newPassword);
            _confirmNewPasswordInput.EnterText(changePassword.confirmNewPassword);
        }

        public void ClickOnSaveButton()
        {
            _saveButton.ClickOnElement();
        }
    }
}