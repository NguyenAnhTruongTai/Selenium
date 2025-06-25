using Automation.BuggyCars.Testing.Models.ManageRating;
using Automation.BuggyCars.Testing.Pages;
using Automation.Core.Drivers;
using Automation.Core.Utilities;
using Automation.Core.WebObject;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public class RatingPage : BasePage
{
    private readonly RatingModel _ratingModel;

    private WebObject _popularMakeImage;
    private WebObject _overallRatingImage = new WebObject(By.XPath("//a[@href='/overall']/img"), "Overall Rating Image");
    private WebObject? _model;
    private WebObject _commentInput = new WebObject(By.Id("comment"), "Comment Input");
    private WebObject _voteButton = new WebObject(By.XPath("//button[text()='Vote!']"), "Vote Button");
    public WebObject PopularMakeImage => _popularMakeImage;
    public WebObject OverallRatingImage => _overallRatingImage;
    public WebObject Model => _model;
    public WebObject CommentInput => _commentInput;
    public WebObject VoteButton => _voteButton;

    public RatingPage(RatingModel model)
    {
        _ratingModel = model ?? throw new ArgumentNullException(nameof(model));

        string makeXPath = $"//img[@title='{_ratingModel.popularMakeImage}']";
        _popularMakeImage = new WebObject(By.XPath(makeXPath), "Popular Make Image");
    }

    public void ClickOnPopularMakeImage()
    {
        _popularMakeImage.ClickOnElement();
    }
    public void ClickOnOverallRatingImage()
    {
        _overallRatingImage.ClickOnElement();
    }
    public void ClickOnModel()
    {
        string modelXPath = $"//td/a[normalize-space()='{_ratingModel.model}']";
        int timeout = int.Parse(ConfigurationUtils.GetSectionValue("WebDriver", "WaitTimeout"));

        while (true)
        {
            try
            {
                var element = BrowserFactory.GetWebDriver().FindElement(By.XPath(modelXPath));
                if (element.Displayed)
                {
                    _model = new WebObject(By.XPath(modelXPath), "Model name");
                    _model.ClickOnElement();
                    return;
                }
            }
            catch (NoSuchElementException)
            {
                var nextButtonBy = By.XPath("//a[text()='»']");
                var nextButtonWebObj = new WebObject(nextButtonBy, "Next Pagination Button");

                try
                {
                    var nextBtn = nextButtonWebObj.WaitForElementToBeEnabled();
                    var classAttr = nextBtn.GetAttribute("class");

                    if (classAttr != null && classAttr.Contains("disabled"))
                        throw new Exception($"Không tìm thấy model '{_ratingModel.model}' sau khi duyệt hết trang.");

                    nextButtonWebObj.ClickOnElement();

                    var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(timeout));
                    wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));

                    bool isModelVisible = wait.Until(driver =>
                    {
                        var modelElements = driver.FindElements(By.XPath(modelXPath));
                        return modelElements.Any();
                    });

                    if (!isModelVisible)
                        throw new Exception($"Không tìm thấy model '{_ratingModel.model}' sau khi sang trang mới.");
                }
                catch (WebDriverTimeoutException)
                {
                    throw new Exception("Không thể click nút Next hoặc trang không load đúng.");
                }
            }
        }
    }
    public void FillComment()
    {
        _commentInput.EnterText(_ratingModel.comment);
    }
    public void ClickOnVoteButton()
    {
        _voteButton.ClickOnElement();
    }
}
