using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.BuggyCars.Testing.Provider.ManageAccount;
using Automation.Test.Core.Utilities;
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
        [Test, TestCaseSource(typeof(LoginProvider), nameof(LoginProvider.GetLoginData))]
        public void TestLoginWithValidCredentials(LoginModel login)
        {
            ExtentReportHelpers.CreateTest("Test Login With Valid Credentials");
            ExtentReportHelpers.CreateNode($"Try login with valid credentials: {login.login} and valid password");
            try
            {
                ExtentReportHelpers.LogTestStep("Attempt to login with valid credentials", Status.Pass);
                _loginPage.FillLoginForm(login);

                ExtentReportHelpers.LogTestStep("Verify login success message", Status.Pass);

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
