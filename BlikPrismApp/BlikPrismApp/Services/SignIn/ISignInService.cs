using System.Threading.Tasks;

namespace BlikPrismApp.Services.SignIn
{
    public interface ISignInService
    {
        Task<bool> SignInAsync(string username, string password);
    }
}