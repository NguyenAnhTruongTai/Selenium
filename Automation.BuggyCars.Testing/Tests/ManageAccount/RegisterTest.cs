using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.BuggyCars.Testing.Provider.ManageAccount;
using Automation.Core.Utilities;
using AventStack.ExtentReports;

namespace Automation.BuggyCars.Testing.Tests.ManageAccount
{
    [TestFixture]
    [Category("RegisterTest")]
    public class RegisterTest : BaseTest
    {
        private RegisterPage _registerPage;

        [SetUp]
        public new void Setup()
        {
            _registerPage = new RegisterPage();
        }
        [Test, TestCaseSource(typeof(RegisterProvider), nameof(RegisterProvider.GetRegisterWithValidData))]
        public void TestRegisterWithValidCredentials(RegisterModel register)
        {
            ExtentReportHelpers.CreateTest($"Register with valid credentials");

            ExtentReportHelpers.CreateNode($"Register with valid credentials: {register.login} and valid password");

            try
            {
                _registerPage.NavigateToRegisterPage();
                ExtentReportHelpers.LogTestStep("Navigated to Register Page", Status.Pass);

                Assertions.ManageAccount.RegisterAssertions.AssertRegisterFormElementsVisible(_registerPage);
                ExtentReportHelpers.LogTestStep("Register form elements are visible", Status.Pass);

                _registerPage.FillRegisterForm(register);
                ExtentReportHelpers.LogTestStep("Register with valid credentials", Status.Pass);
                _registerPage.ClickOnRegisterButton();
                ExtentReportHelpers.LogTestStep("Clicked on Register button", Status.Pass);

                Assertions.ManageAccount.RegisterAssertions.AssertUserRegisteredSuccessfully(_registerPage, register);
                ExtentReportHelpers.LogTestStep("Verify registration success message", Status.Pass);

                ExtentReportHelpers.LogTestStep("Registration successful", Status.Pass);
            }
            catch (Exception ex)
            {
                ExtentReportHelpers.LogException(ex);
                throw;
            }
        }
    }
}
