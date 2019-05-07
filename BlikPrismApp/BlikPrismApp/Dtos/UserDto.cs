using Refit;

namespace BlikPrismApp.Services.SignIn
{
    public class UserDto
    {
        [AliasAs("username")]
        public string Username { get; set; }

        [AliasAs("password")]
        public string Password { get; set; }

        public UserDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}