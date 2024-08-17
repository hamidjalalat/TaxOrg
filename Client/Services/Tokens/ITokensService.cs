using System.Threading.Tasks;

namespace Client.Services.Tokens
{
    public interface ITokensService
    {
        Task<ViewModels.Users.UserAuthenticationLoginResultViewModel> GetToken();
        Task SetToken(ViewModels.Users.UserAuthenticationLoginResultViewModel loginResult);
        Task RemoveToken();

        // ***************************************
        Task<System.Guid?> GetUserId();
        Task SetUserId(string userId);
        Task RemoveUserId();
    }
}
