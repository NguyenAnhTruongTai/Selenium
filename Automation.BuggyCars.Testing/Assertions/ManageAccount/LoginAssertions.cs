

using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Assertions.ManageAccount
{
    public static class LoginAssertions
    {
        public static void AssertLoginFormElementsVisible(LoginPage loginPage)
        {

            Assert.That(loginPage.LoginInput.IsElementDisplayed(), Is.True, "Login field is not visible");
            Assert.That(loginPage.PasswordInput.IsElementDisplayed(), Is.True, "Password field is not visible");
            Assert.That(loginPage.LoginButton.IsElementDisplayed(), Is.True, "Login button is not visible");

        }
        public static void AssertUserLoggedInSuccessfully(LoginPage loginPage, LoginModel login)
        {
            string actualGreeting = loginPage.GreetingText.GetTextFromElement().Trim();
            string expectedGreeting = $"Hi, {login.firstName}";

            Assert.That(actualGreeting, Is.EqualTo(expectedGreeting), $"Greeting text is not match. Expected: '{expectedGreeting}', Actual: '{actualGreeting}'");
            Assert.That(loginPage.ProfileButton.IsElementDisplayed(), Is.True, "Profile button is not visible");
            Assert.That(loginPage.LogoutButton.IsElementDisplayed(), Is.True, "Logout button is not visible");
        }

    }
}
