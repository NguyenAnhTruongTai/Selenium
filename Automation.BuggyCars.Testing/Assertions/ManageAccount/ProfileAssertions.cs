using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Assertions.ManageAccount
{
    public static class ProfileAssertions
    {
        public static void AssertProfileFormElementsVisible(ProfilePage profilePage)
        {

            Assert.That(profilePage.FirstNameInput.IsElementDisplayed(), Is.True, "First Name field is not visible");
            Assert.That(profilePage.LastNameInput.IsElementDisplayed(), Is.True, "Last Name field is not visible");
            Assert.That(profilePage.GenderDropdown.IsElementDisplayed(), Is.True, "Email field is not visible");
            Assert.That(profilePage.AgeInput.IsElementDisplayed(), Is.True, "Phone field is not visible");
            Assert.That(profilePage.AddressInput.IsElementDisplayed(), Is.True, "Age field is not visible");
            Assert.That(profilePage.PhoneInput.IsElementDisplayed(), Is.True, "Save button is not visible");
            Assert.That(profilePage.HobbyDropdown.IsElementDisplayed(), Is.True, "Hobby field is not visible");
        }
        public static void AssertProfileSavedSuccessfully(ProfilePage profilePage)
        {
            Assert.That(profilePage.SuccessMessage.IsElementDisplayed(), Is.True, "Profile saved success text is not visible");
        }
    }
}
