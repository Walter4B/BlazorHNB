using BlazorAppHNB.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace BlazorAppHNB.Server.Authentication
{
    public class JwtAuthenticationManager
    {
        public const string JWT_SECURITY_KEY = "HG95fGp0THavQb6vvVhu3xGku2lOqXcrP06m1z";
        private const int JWT_TOKEN_VALIDITY_MINS = 60;

        private UserAccountService _userAccountService;

        public JwtAuthenticationManager(UserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public UserSession? GenerateJwtToken(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || (string.IsNullOrWhiteSpace(password)))
            { 
                return null;
            }

            var userAccount = _userAccountService.GetUserAccountByUserName(userName);
            Console.WriteLine(password);
            if (userAccount == null || !BCrypt.Net.BCrypt.Verify(userAccount.Password, password)) 
            {
                return null;
            }

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);

            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.UserName)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials= signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            var userSession = new UserSession
            {
                UserName = userAccount.UserName,
                Token = token,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };

            return userSession;
        }
    }
}
