using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlikPrismApp.Services.SignIn
{
    public interface ISignInApiService
    {
        [Post("/signin")]
        Task<HttpResponseMessage> SignInAsync([Body] object userData);
    }

    public class SignInService : ISignInService
    {
        private readonly ISignInApiService apiService;

        public SignInService()
        {
            apiService = RestService.For<ISignInApiService>(new HttpClient
            {
                BaseAddress = new Uri(Constants.ApiUrl)
            });
        }

        public async Task<bool> SignInAsync(string username, string password)
        {
            var response = await apiService.SignInAsync(new { username, password });
            return response.IsSuccessStatusCode;
        }
    }
}
