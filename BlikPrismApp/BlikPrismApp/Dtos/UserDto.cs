using Newtonsoft.Json;

namespace BlikPrismApp.Services.SignIn
{
    public class UserDto
    {
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public UserDto(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}