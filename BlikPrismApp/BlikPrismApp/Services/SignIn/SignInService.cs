using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlikPrismApp.Services.SignIn
{
    public interface ISignInApiService
    {
        [Post("/signin")]
        Task<HttpResponseMessage> SignInAsync([Body] UserDto userData);
    }

    public class SignInService : ISignInService
    {
        private readonly ISignInApiService apiService;

        public SignInService()
        {
            apiService = RestService.For<ISignInApiService>(new HttpClient
            { BaseAddress = Constants.ApiUrl });
        }

        public async Task<bool> SignInAsync(string username, string password)
        {
            var userData = new UserDto(username, password);
            var response = await apiService.SignInAsync(userData);
            return response.IsSuccessStatusCode;
        }
    }
}
