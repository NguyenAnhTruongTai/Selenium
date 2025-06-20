using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.BuggyCars.Testing.Provider.ManageAccount;
using Automation.Core.Utilities;
using AventStack.ExtentReports;

namespace Automation.BuggyCars.Testing.Tests.ManageAccount
{
    [TestFixture]
    [Category("LoginTest")]
    public class LoginTest : BaseTest
    {
        private LoginPage _loginPage;

        [SetUp]
        public new void Setup()
        {
            _loginPage = new LoginPage();
        }
        [Test, TestCaseSource(typeof(LoginProvider), nameof(LoginProvider.GetLoginWithValidData))]
        public void TestLoginWithValidCredentials(LoginModel login)
        {
            ExtentReportHelpers.CreateTest($"Login with valid credentials");

            ExtentReportHelpers.CreateNode($"Login with valid credentials: {login.login} and valid password");

            try
            {
                Assertions.ManageAccount.LoginAssertions.AssertLoginFormElementsVisible(_loginPage);
                ExtentReportHelpers.LogTestStep("Login form elements are visible", Status.Pass);
                _loginPage.FillLoginForm(login);
                ExtentReportHelpers.LogTestStep("Login with valid credentials", Status.Pass);

                ExtentReportHelpers.LogTestStep("Verify login success message", Status.Pass);
                Assertions.ManageAccount.LoginAssertions.AssertUserLoggedInSuccessfully(_loginPage, login);

                ExtentReportHelpers.LogTestStep("Login successful", Status.Pass);
            }
            catch (Exception ex)
            {
                ExtentReportHelpers.LogException(ex);
                throw;
            }
        }
    }
}
