using BlikPrismApp.Services.SignIn;
using Refit;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlikPrismApp.Services.Blik
{
    public interface IBlikApiService
    {
        [Post("/blik")]
        Task<int> GetBlikCode([Body] UserDto userData);
    }

    public class BlikService : IBlikService
    {
        private readonly IBlikApiService _apiService;

        public BlikService()
        {
            _apiService = RestService.For<IBlikApiService>(new HttpClient
            { BaseAddress = Constants.ApiUrl });
        }

        public BlikService(IBlikApiService apiService) => _apiService = apiService;

        public async Task<int> GetBlikCodeAsync(UserDto userDto) =>
            await _apiService.GetBlikCode(userDto);
    }
}
