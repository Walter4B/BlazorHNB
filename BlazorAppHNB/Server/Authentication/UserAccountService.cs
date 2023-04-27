using BCrypt.Net;

namespace BlazorAppHNB.Server.Authentication
{
    public class UserAccountService
    {
        private List<UserAccount> userAccountsList;

        public UserAccountService()
        {
            userAccountsList = new List<UserAccount>
            { 
                new UserAccount{ UserName = "user", Password = "password", PasswordHash = BCrypt.Net.BCrypt.HashPassword("password")}
            };
            Console.WriteLine(userAccountsList[0].PasswordHash);
        }

        public UserAccount? GetUserAccountByUserName(string name)
        {
            return userAccountsList.FirstOrDefault(x => x.UserName == name);
        }
    }
}
