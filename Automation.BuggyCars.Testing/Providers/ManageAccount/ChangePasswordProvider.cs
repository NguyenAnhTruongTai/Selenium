

using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.Utils;

namespace Automation.BuggyCars.Testing.Provider.ManageAccount
{
    public class ChangePasswordProvider
    {
        public static IEnumerable<TestCaseData> ChangePasswordThenLoginAgain()
        {
            var passwordOfLoggedInUser = JsonUtils.GetJsonData("ManageAccount", "LoginData.json", "loginWithValidData");
            var passwordDataToChange = JsonUtils.GetJsonData("ManageAccount", "ChangePasswordData.json", "changePasswordWithValidData");
            var loginAfterChangePassword = JsonUtils.GetJsonData("ManageAccount", "ChangePasswordData.json", "loginAfterChangePassword")
                                      as IEnumerable<Dictionary<string, string>>;
            var loginAfterChangePasswordList = loginAfterChangePassword?.ToList();
            int total = new[] { passwordOfLoggedInUser.Count, passwordDataToChange.Count, loginAfterChangePasswordList?.Count ?? 0 }.Min();

            for (int i = 0; i < total; i++)
            {
                var loginModel = new LoginModel
                {
                    login = passwordOfLoggedInUser[i].ContainsKey("Login") ? passwordOfLoggedInUser[i]["Login"]! : throw new KeyNotFoundException("Login not found."),
                    password = passwordOfLoggedInUser[i].ContainsKey("Password") ? passwordOfLoggedInUser[i]["Password"]! : throw new KeyNotFoundException("Password not found."),
                    firstName = passwordOfLoggedInUser[i].ContainsKey("FirstName") ? passwordOfLoggedInUser[i]["FirstName"]! : throw new KeyNotFoundException("FirstName not found.")
                };

                var changePasswordModel = new ChangePasswordModel
                {
                    currentPassword = passwordDataToChange[i].ContainsKey("CurrentPassword") ? passwordDataToChange[i]["CurrentPassword"]! : throw new KeyNotFoundException("CurrentPassword not found."),
                    newPassword = passwordDataToChange[i].ContainsKey("NewPassword") ? passwordDataToChange[i]["NewPassword"]! : throw new KeyNotFoundException("NewPassword not found."),
                    confirmNewPassword = passwordDataToChange[i].ContainsKey("ConfirmNewPassword") ? passwordDataToChange[i]["ConfirmNewPassword"]! : throw new KeyNotFoundException("ConfirmNewPassword not found.")
                };
                var loginAfterChangePasswordModel = new ChangePasswordModel
                {
                    currentPassword = loginAfterChangePasswordList![i].ContainsKey("CurrentPassword") ? loginAfterChangePasswordList[i]["CurrentPassword"]! : throw new KeyNotFoundException("CurrentPassword not found."),
                    newPassword = loginAfterChangePasswordList[i].ContainsKey("NewPassword") ? loginAfterChangePasswordList[i]["NewPassword"]! : throw new KeyNotFoundException("NewPassword not found."),
                    confirmNewPassword = loginAfterChangePasswordList[i].ContainsKey("ConfirmNewPassword") ? loginAfterChangePasswordList[i]["ConfirmNewPassword"]! : throw new KeyNotFoundException("ConfirmNewPassword not found.")
                };

                yield return new TestCaseData(loginModel, changePasswordModel, loginAfterChangePasswordModel)
                             .SetName($"ChangePasswordAndReLogin_{loginModel.login}");
            }
        }
    }
}