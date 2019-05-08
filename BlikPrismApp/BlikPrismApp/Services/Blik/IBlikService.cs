using BlikPrismApp.Services.SignIn;
using System.Threading.Tasks;

namespace BlikPrismApp.Services.Blik
{
    public interface IBlikService
    {
        Task<int> GetBlikCodeAsync(UserDto userDto);
    }
}