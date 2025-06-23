using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.Utils;

namespace Automation.BuggyCars.Testing.Provider.ManageAccount
{
    public class RegisterProvider
    {
        public static IEnumerable<TestCaseData> GetRegisterWithValidData()
        {
            var testData = JsonUtils.GetJsonData("ManageAccount", "RegisterData.json", "registerWithValidData") as IEnumerable<Dictionary<string, string>>;

            foreach (var result in testData)
            {
                var dto = new RegisterModel
                {
                    login = result.ContainsKey("Login") ? result["Login"]! : throw new KeyNotFoundException("Login not found."),
                    firstName = result.ContainsKey("FirstName") ? result["FirstName"]! : throw new KeyNotFoundException("FirstName not found."),
                    lastName = result.ContainsKey("LastName") ? result["LastName"]! : throw new KeyNotFoundException("LastName not found."),
                    password = result.ContainsKey("Password") ? result["Password"]! : throw new KeyNotFoundException("Password not found."),
                    confirmPassword = result.ContainsKey("ConfirmPassword") ? result["ConfirmPassword"]! : throw new KeyNotFoundException("ConfirmPassword not found.")
                };

                yield return new TestCaseData(dto)
                             .SetName($"Register - Valid: {dto.login ?? "(empty)"}");
            }
        }
    }
}
