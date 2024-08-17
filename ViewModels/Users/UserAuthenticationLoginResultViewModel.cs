
using System;

namespace ViewModels.Users
{
    public class UserAuthenticationLoginResultViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
