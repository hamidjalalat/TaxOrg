using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Linq;
using System;
using System.Text;

namespace Infrastructure
{
    public class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payLoad = jwt.Split('.')[1];
            var jsonByte = ParseBase64WithoutPadding(payLoad);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonByte);
            ExtractRoleFromJwt(claims, keyValuePairs);
            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            try
            {
                base64 = base64
                    .Replace('-', '+')
                    .Replace(' ', '+')
                    .Replace('_', '/')
                    ;

                switch (base64.Length % 4)
                {
                    case 2:
                        base64 += "=="; break;
                    case 3:
                        base64 += "="; break;
                    default:
                        break;
                }
                return Convert.FromBase64String(base64);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void ExtractRoleFromJwt(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out var role);
            if (role is not null)
            {
                var parsedRoles = role.ToString().Trim().TrimStart(trimChar: '[').TrimEnd(trimChar: ']').Split(separator: ',');
                if (parsedRoles.Length > 1)
                {
                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim(trimChar: '"')));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
                }
                keyValuePairs.Remove(ClaimTypes.Role);
            }
        }
    }
}
