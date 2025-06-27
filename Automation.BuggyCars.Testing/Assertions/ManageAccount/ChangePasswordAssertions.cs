

using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Assertions.ManageAccount
{
    public static class ChangePasswordAssertions
    {
        public static void AssertChangePasswordFormElementsVisible(ChangePasswordPage changePasswordPage)
        {

            Assert.That(changePasswordPage.CurrentPasswordInput.IsElementDisplayed(), Is.True, "Current Password field is not visible");
            Assert.That(changePasswordPage.NewPasswordInput.IsElementDisplayed(), Is.True, "New Password field is not visible");
            Assert.That(changePasswordPage.ConfirmNewPasswordInput.IsElementDisplayed(), Is.True, "Confirm New Password field is not visible");
            Assert.That(changePasswordPage.SaveButton.IsElementDisplayed(), Is.True, "Save button is not visible");
        }
        public static void AssertChangePasswordSuccess(ChangePasswordPage changePasswordPage, ChangePasswordModel changePassword)
        {
            Assert.That(changePasswordPage.SuccessMessage.IsElementDisplayed(), Is.True, "Success message is not visible");
        }
    }
}
