using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Anemic.Entities;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(
            UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }


        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }



        public async Task<Result<IdentityResult>> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            var response = new FluentResults.Result<IdentityResult>();
            return (response.WithValue(result));
        }

        public async Task<(FluentResults.Result<IdentityResult> Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new User
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);
            var response = new FluentResults.Result<IdentityResult>();
            return (response.WithValue(result), user.Id);
        }

        public async Task<FluentResults.Result<IdentityResult>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var response = new FluentResults.Result<IdentityResult>();
            return user != null ? await DeleteUserAsync(user) : response.WithSuccess(Resources.Messages.Successes.SuccessDelete);
        }
    }
}