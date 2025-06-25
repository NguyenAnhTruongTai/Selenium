using Automation.BuggyCars.Testing.Models.ManageRating;
using Automation.Core.Utils;

namespace Automation.BuggyCars.Testing.Provider.ManageRating
{
    public class RatingProvider
    {
        public static IEnumerable<TestCaseData> GetRatingData()
        {
            var testData = JsonUtils.GetJsonData("ManageRating", "RatingData.json", "RatingData") as IEnumerable<Dictionary<string, string>>;

            foreach (var result in testData)
            {
                var dto = new RatingModel
                {
                    popularMakeImage = result.ContainsKey("PopularMakeImage") ? result["PopularMakeImage"]! : throw new KeyNotFoundException("PopularMakeImage not found."),
                    comment = result.ContainsKey("Comment") ? result["Comment"]! : throw new KeyNotFoundException("Comment not found."),
                    model = result.ContainsKey("Model") ? result["Model"]! : throw new KeyNotFoundException("Model not found.")
                };
                yield return new TestCaseData(dto)
                             .SetName($"Rating - Valid: {dto.popularMakeImage ?? "(empty)"}");
            }
        }
    }
}
