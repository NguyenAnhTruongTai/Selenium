using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.Utils;

namespace Automation.BuggyCars.Testing.Provider.ManageAccount
{
    public class LoginProvider
    {
        public static IEnumerable<TestCaseData> GetLoginData()
        {
            var testData = JsonUtils.GetJsonData("ManageAccount", "LoginData.json", "loginWithValidData") as IEnumerable<Dictionary<string, string>>;

            foreach (var result in testData)
            {
                var dto = new LoginModel
                {
                    login = result.ContainsKey("Login") ? result["Login"]! : throw new KeyNotFoundException("Login not found."),
                    password = result.ContainsKey("Password") ? result["Password"]! : throw new KeyNotFoundException("Password not found."),
                };

                yield return new TestCaseData(dto)
                             .SetName($"Login - Invalid: {dto.login ?? "(empty)"}");
            }
        }
    }
}
