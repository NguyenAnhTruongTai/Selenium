namespace Automation.BuggyCars.Testing.Models.ManageAccount
{
    public class ChangePasswordModel
    {
        public required string currentPassword { get; set; }
        public required string newPassword { get; set; }
        public required string confirmNewPassword { get; set; }
    }
}