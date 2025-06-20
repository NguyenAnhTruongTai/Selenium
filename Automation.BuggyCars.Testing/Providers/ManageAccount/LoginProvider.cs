using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.Utils;

namespace Automation.BuggyCars.Testing.Provider.ManageAccount
{
    public class LoginProvider
    {
        public static IEnumerable<TestCaseData> GetLoginWithValidData()
        {
            var testData = JsonUtils.GetJsonData("ManageAccount", "LoginData.json", "loginWithValidData") as IEnumerable<Dictionary<string, string>>;

            foreach (var result in testData)
            {
                var dto = new LoginModel
                {
                    login = result.ContainsKey("Login") ? result["Login"]! : throw new KeyNotFoundException("Login not found."),
                    password = result.ContainsKey("Password") ? result["Password"]! : throw new KeyNotFoundException("Password not found."),
                    firstName = result.ContainsKey("FirstName") ? result["FirstName"]! : throw new KeyNotFoundException("FirstName not found.")

                };

                yield return new TestCaseData(dto)
                             .SetName($"Login - Valid: {dto.login ?? "(empty)"}");
            }
        }
    }
}
