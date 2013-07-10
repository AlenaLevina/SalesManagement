namespace SalesManagement.MvcApplication.ViewModels.Account
{
    public class RegisterViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public bool SuccessfullyRegistered { get; set; }
        public string Role { get; set; }
    }
}