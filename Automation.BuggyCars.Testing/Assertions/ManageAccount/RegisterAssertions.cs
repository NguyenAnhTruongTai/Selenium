using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Assertions.ManageAccount
{
    public static class RegisterAssertions
    {
        public static void AssertRegisterFormElementsVisible(RegisterPage registerPage)
        {

            Assert.That(registerPage.LoginInput.IsElementDisplayed(), Is.True, "Login field is not visible");
            Assert.That(registerPage.FirstNameInput.IsElementDisplayed(), Is.True, "First Name field is not visible");
            Assert.That(registerPage.LastNameInput.IsElementDisplayed(), Is.True, "Last Name field is not visible");
            Assert.That(registerPage.PasswordInput.IsElementDisplayed(), Is.True, "Password field is not visible");
            Assert.That(registerPage.ConfirmPasswordInput.IsElementDisplayed(), Is.True, "Confirm Password field is not visible");
            Assert.That(registerPage.RegisterButton.IsElementDisplayed(), Is.True, "Register button is not visible");
            Assert.That(registerPage.CancelButton.IsElementDisplayed(), Is.True, "Cancel button is not visible");
        }
        public static void AssertUserRegisteredSuccessfully(RegisterPage registerPage, RegisterModel register)
        {
            Assert.That(registerPage.RegistrationSuccessText.IsElementDisplayed(), Is.True, "Registration success text is not visible");
        }
    }
}
