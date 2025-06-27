

using Automation.BuggyCars.Testing.Assertions.ManageAccount;
using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.BuggyCars.Testing.Provider.ManageAccount;
using Automation.Core.Utilities;
using AventStack.ExtentReports;

namespace Automation.BuggyCars.Testing.Tests
{
    [TestFixture]
    [Category("ProfileTest")]
    public class ProfileTest : BaseTest
    {
        private ProfilePage _profilePage;
        private LoginPage _loginPage;

        [SetUp]
        public new void Setup()
        {
            _loginPage = new LoginPage();
            _profilePage = new ProfilePage();
        }

        [Test, TestCaseSource(typeof(ProfileProvider), nameof(ProfileProvider.GetProfileWithValidData))]
        public void TestProfileUpdate(LoginModel login, ProfileModel profile)
        {
            ExtentReportHelpers.CreateTest("Test Profile Update");
            ExtentReportHelpers.CreateNode("Execute Test Steps");
            try
            {
                _loginPage.FillLoginForm(login);
                ExtentReportHelpers.LogTestStep("Login with valid credentials", Status.Pass);

                _profilePage.NavigateToProfilePage();
                ExtentReportHelpers.LogTestStep("Navigate to Profile Page", Status.Pass);
                ProfileAssertions.AssertProfileFormElementsVisible(_profilePage);
                _profilePage.FillProfileFormWithAllFields(profile);
                ExtentReportHelpers.LogTestStep("Fill Profile Form", Status.Pass);
                _profilePage.ClickOnSaveButton();
                ExtentReportHelpers.LogTestStep("Click on Save button", Status.Pass);
                ProfileAssertions.AssertProfileSavedSuccessfully(_profilePage);
            }
            catch (Exception ex)
            {
                ExtentReportHelpers.LogException(ex);
                throw;
            }
        }
    }
}