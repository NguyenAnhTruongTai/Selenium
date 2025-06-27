using Automation.Core.WebObject;

namespace Automation.BuggyCars.Testing.Assertions.ManageRating
{
    public static class RatingAssertions
    {
        public static void AssertRatingFormElementsVisible(RatingPage ratingPage)
        {
            Assert.That(ratingPage.CommentInput.IsElementDisplayed(), Is.True, "Comment field is not visible");
            Assert.That(ratingPage.VoteButton.IsElementDisplayed(), Is.True, "Vote button is not visible");
        }
        public static void AssertRatingSuccess(RatingPage ratingPage)
        {
            Assert.That(ratingPage.RatingSuccessMessage.IsElementDisplayed(), Is.True, "Rating success message is not visible");
        }
    }
}
