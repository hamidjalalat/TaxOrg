using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Common
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        public string IPAddress => _httpContextAccessor.HttpContext?.IPAddress() ?? "";
        public string ComputerName => Environment.GetEnvironmentVariable("COMPUTERNAME")??"";
    }
}
