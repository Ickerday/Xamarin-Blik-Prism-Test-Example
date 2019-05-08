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
        private readonly ISignInApiService _apiService;

        public SignInService(ISignInApiService apiService) => _apiService = apiService;

        public SignInService()
        {
            _apiService = RestService.For<ISignInApiService>(new HttpClient
            { BaseAddress = Constants.ApiUrl });
        }

        public async Task<bool> SignInAsync(UserDto userDto)
        {
            var response = await _apiService.SignInAsync(userDto);
            return response.IsSuccessStatusCode;
        }
    }
}
