namespace BlazorAppHNB.Server.Authentication
{
    public class UserAccountService
    {
        private List<UserAccount> userAccountsList;

        public UserAccountService()
        {
            userAccountsList = new List<UserAccount>
            {
                new UserAccount{ UserName = "user", Password = "password"}
            };
        }

        public UserAccount? GetUserAccountByUserName(string name)
        {
            return userAccountsList.FirstOrDefault(x => x.UserName == name);
        }
    }
}
