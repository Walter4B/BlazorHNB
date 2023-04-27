namespace BlazorAppHNB.Server.Authentication
{
    public class UserAccount
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
    }
}
