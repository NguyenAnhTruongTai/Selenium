

using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.BuggyCars.Testing.Provider.ManageAccount;
using Automation.Core.Utilities;
using AventStack.ExtentReports;

namespace Automation.BuggyCars.Testing.Tests
{
    [TestFixture]
    [Category("ChangePasswordTest")]
    public class ChangePasswordTest : BaseTest
    {
        private ChangePasswordPage _changePasswordPage;
        private LoginPage _loginPage;
        private LogoutPage _logoutPage;

        [SetUp]
        public new void Setup()
        {
            _loginPage = new LoginPage();
            _changePasswordPage = new ChangePasswordPage();
            _logoutPage = new LogoutPage();
        }

        [Test, TestCaseSource(typeof(ChangePasswordProvider), nameof(ChangePasswordProvider.ChangePasswordThenLoginAgain))]
        public void TestChangePasswordSuccessfully(LoginModel login, ChangePasswordModel changePassword, LoginModel loginAfterChangePassword)
        {
            ExtentReportHelpers.CreateTest("Test Change Password Successfully");
            ExtentReportHelpers.CreateNode("Execute Test Steps");
            try
            {
                _loginPage.FillLoginForm(login);
                ExtentReportHelpers.LogTestStep("Login with valid credentials", Status.Pass);

                _changePasswordPage.NavigateToProfilePage();
                ExtentReportHelpers.LogTestStep("Navigate to Change Password screen", Status.Pass);

                _changePasswordPage.FillChangePasswordForm(changePassword);
                ExtentReportHelpers.LogTestStep("Fill Change Password Form", Status.Pass);

                _changePasswordPage.ClickOnSaveButton();
                ExtentReportHelpers.LogTestStep("Click on Save button", Status.Pass);
            }
            catch (Exception ex)
            {
                ExtentReportHelpers.LogException(ex);
                throw;
            }
        }
    }
}