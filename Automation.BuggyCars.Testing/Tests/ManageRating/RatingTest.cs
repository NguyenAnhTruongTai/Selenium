using Automation.BuggyCars.Testing.Assertions.ManageRating;
using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.BuggyCars.Testing.Models.ManageRating;
using Automation.BuggyCars.Testing.Pages.ManageAccount;
using Automation.BuggyCars.Testing.Provider.ManageAccount;
using Automation.BuggyCars.Testing.Provider.ManageRating;
using Automation.Core.Utilities;
using AventStack.ExtentReports;

namespace Automation.BuggyCars.Testing.Tests.ManageRating
{
    [TestFixture]
    [Category("RatingTest")]
    public class RatingTest : BaseTest
    {
        private LoginPage _loginPage;
        private RatingPage _ratingPage;
        [SetUp]
        public new void Setup()
        {
            _loginPage = new LoginPage();
            DriverUtil.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl"));

            var _loginTestCase = LoginProvider.GetLoginWithValidData().FirstOrDefault();
            var _login = _loginTestCase?.Arguments[0] as LoginModel;

            if (_login == null)
            {
                throw new Exception("LoginModel is null.");
            }

            _loginPage.FillLoginForm(_login);

        }
        [Test, TestCaseSource(typeof(RatingProvider), nameof(RatingProvider.GetRatingData))]
        public void TestRatingWhenClickOnOverallImage(RatingModel rating)
        {
            _ratingPage = new RatingPage(rating);

            ExtentReportHelpers.CreateTest($"Rating a car");

            ExtentReportHelpers.CreateNode($"Rating Make: {rating.popularMakeImage} Rating comment: {rating.comment}");

            try
            {
                _ratingPage.ClickOnOverallRatingImage();
                ExtentReportHelpers.LogTestStep("Clicked on Overall Rating Image", Status.Pass);
                _ratingPage.ClickOnModel();
                ExtentReportHelpers.LogTestStep("Clicked on Model", Status.Pass);
                RatingAssertions.AssertRatingFormElementsVisible(_ratingPage);
                _ratingPage.FillComment();
                ExtentReportHelpers.LogTestStep("Filled Comment", Status.Pass);
                _ratingPage.ClickOnVoteButton();
                ExtentReportHelpers.LogTestStep("Clicked on Vote Button", Status.Pass);
                RatingAssertions.AssertRatingSuccess(_ratingPage);
            }
            catch (Exception ex)
            {
                ExtentReportHelpers.LogException(ex);
                throw;
            }
        }
    }
}
