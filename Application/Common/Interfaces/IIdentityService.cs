using FluentResults;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<(Result<IdentityResult> Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result<IdentityResult>> DeleteUserAsync(string userId);
    }
}