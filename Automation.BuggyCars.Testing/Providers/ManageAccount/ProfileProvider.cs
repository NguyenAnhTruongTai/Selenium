using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.Utils;

namespace Automation.BuggyCars.Testing.Provider.ManageAccount
{
    public class ProfileProvider
    {
        public static IEnumerable<TestCaseData> GetProfileWithValidData()
        {
            var loginData = JsonUtils.GetJsonData("ManageAccount", "LoginData.json", "loginWithValidData") as IEnumerable<Dictionary<string, string>>;

            var profileData = JsonUtils.GetJsonData("ManageAccount", "ProfileData.json", "EditProfileWithAllData") as IEnumerable<Dictionary<string, string>>;
            int total = new[] { loginData.Count(), profileData.Count() }.Min();

            var loginList = loginData.ToList();
            var profileList = profileData.ToList();

            for (int i = 0; i < total; i++)
            {
                var resultProfileList = profileList[i];
                var resultLoginList = loginList[i];
                var loginModel = new LoginModel
                {
                    login = resultLoginList.ContainsKey("Login") ? resultLoginList["Login"]! : throw new KeyNotFoundException("Login not found."),
                    password = resultLoginList.ContainsKey("Password") ? resultLoginList["Password"]! : throw new KeyNotFoundException("Password not found."),
                    firstName = resultLoginList.ContainsKey("FirstName") ? resultLoginList["FirstName"]! : throw new KeyNotFoundException("FirstName not found.")
                };
                var profileModel = new ProfileModel
                {
                    lastName = resultProfileList.ContainsKey("LastName") ? resultProfileList["LastName"]! : throw new KeyNotFoundException("LastName not found."),
                    gender = resultProfileList.ContainsKey("Gender") ? resultProfileList["Gender"]! : throw new KeyNotFoundException("Gender not found."),
                    age = resultProfileList.ContainsKey("Age") ? resultProfileList["Age"]! : throw new KeyNotFoundException("Age not found."),
                    address = resultProfileList.ContainsKey("Address") ? resultProfileList["Address"]! : throw new KeyNotFoundException("Address not found."),
                    phone = resultProfileList.ContainsKey("Phone") ? resultProfileList["Phone"]! : throw new KeyNotFoundException("Phone not found."),
                    hobby = resultProfileList.ContainsKey("Hobby") ? resultProfileList["Hobby"]! : throw new KeyNotFoundException("Hobby not found.")
                };
                yield return new TestCaseData(loginModel, profileModel)
                    .SetName($"Login: {loginModel.login}, Profile LastName: {profileModel.lastName}");
            }
        }
    }
}