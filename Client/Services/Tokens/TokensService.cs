using Blazored.LocalStorage;
using Infrastructure;
using System.Threading.Tasks;
using ViewModels.Users;

namespace Client.Services.Tokens
{
    public class TokensService : ITokensService
    {
        private readonly ILocalStorageService _localStorageService;

        public TokensService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<UserAuthenticationLoginResultViewModel> GetToken()
        {
            return await _localStorageService.GetItemAsync<UserAuthenticationLoginResultViewModel>(Utility.Key_Token);
        }

        public async Task SetToken(UserAuthenticationLoginResultViewModel loginResult)
        {
            await _localStorageService.SetItemAsync(Utility.Key_Token, loginResult);
        }

        public async Task RemoveToken()
        {
            await _localStorageService.RemoveItemAsync(Utility.Key_Token);
        }

        // ***************************************
        public async Task<System.Guid?> GetUserId()
        {
            return await _localStorageService.GetItemAsync<System.Guid>(Utility.Key_UserId);
        }

        public async Task SetUserId(string userId)
        {
            await _localStorageService.SetItemAsync(Utility.Key_UserId, userId);
        }

        public async Task RemoveUserId()
        {
            await _localStorageService.RemoveItemAsync(Utility.Key_UserId);
        }
    }
}
