using Domain.Anemic.Entities;
using Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Infrastructure.Common.Anemic
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        public UserClaimsPrincipalFactory(
            UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor)
                : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var rolesList = await UserManager.GetRolesAsync(user);
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(MyClaimTypes.FullName, $"{user?.FirstName} {user?.LastName}"));
            identity.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", rolesList)));
            return identity;
        }
    }
}
