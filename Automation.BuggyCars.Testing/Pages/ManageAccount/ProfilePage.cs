using OpenQA.Selenium;
using Automation.BuggyCars.Testing.Models.ManageAccount;
using Automation.Core.WebObject;
using Automation.BuggyCars.Testing.Extensions;

namespace Automation.BuggyCars.Testing.Pages.ManageAccount
{
    public class ProfilePage : BasePage
    {
        private WebObject _profileButton = new WebObject(By.XPath("//a[text()='Profile']"), "Navigate to Profile Page Button");
        private WebObject _firstNameInput = new WebObject(By.Id("firstName"), "First Name Input");
        private WebObject _lastNameInput = new WebObject(By.Id("lastName"), "Last Name Input");
        private WebObject _genderInput = new WebObject(By.Id("gender"), "Gender Input");
        private WebObject _ageInput = new WebObject(By.Id("age"), "Age Input");
        private WebObject _addressInput = new WebObject(By.Id("address"), "Address Input");
        private WebObject _phoneInput = new WebObject(By.Id("phone"), "Phone Input");
        private WebObject _hobbyInput = new WebObject(By.Id("hobby"), "Hobby Input");
        private WebObject _saveButton = new WebObject(By.XPath("//button[text()='Save']"), "Save Button");

        public WebObject SaveButton => _saveButton;
        public WebObject ProfileButton => _profileButton;
        public WebObject FirstNameInput => _firstNameInput;
        public WebObject LastNameInput => _lastNameInput;
        public WebObject GenderInput => _genderInput;
        public WebObject AgeInput => _ageInput;
        public WebObject AddressInput => _addressInput;
        public WebObject PhoneInput => _phoneInput;
        public WebObject HobbyInput => _hobbyInput;

        public ProfilePage() { }

        public void NavigateToProfilePage()
        {
            _profileButton.ClickOnElement();
        }

        public void FillProfileFormWithAllFields(ProfileModel profile)
        {
            if (profile.firstName != null)
            {
                _firstNameInput.ClearText();
                _firstNameInput.EnterText(profile.firstName);
            }
            if (profile.lastName != null)
            {
                _lastNameInput.ClearText();
                _lastNameInput.EnterText(profile.lastName);
            }

            if (profile.gender != null)
            {
                _genderInput.ClearText();
                _genderInput.PressEnter(profile.gender);
            }

            if (profile.age != null)
            {
                _ageInput.ClearText();
                _ageInput.EnterText(profile.age);
            }

            if (profile.address != null)
            {
                _addressInput.ClearText();
                _addressInput.EnterText(profile.address);
            }

            if (profile.phone != null)
            {
                _phoneInput.ClearText();
                _phoneInput.EnterText(profile.phone);
            }

            if (profile.hobby != null)
            {
                _hobbyInput.SelectDropdownByText(profile.hobby);
            }

        }

        public void ClickOnSaveButton()
        {
            _saveButton.ClickOnElement();
        }
    }
}