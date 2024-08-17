using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class BearerTokensOptions
    {
        public string Key { set; get; }
        public string? Issuer { set; get; }
        public string? Audience { set; get; }
        public int AccessTokenExpirationMinutes { set; get; }
        public int RefreshTokenValidityInDays { set; get; }

    }
}
