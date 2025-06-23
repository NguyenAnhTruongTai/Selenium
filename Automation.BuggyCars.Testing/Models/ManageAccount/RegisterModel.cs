namespace Automation.BuggyCars.Testing.Models.ManageAccount
{
    public class RegisterModel
    {
        public required string login { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string password { get; set; }
        public required string confirmPassword { get; set; }
    }
}