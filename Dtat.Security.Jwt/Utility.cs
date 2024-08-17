using System.Linq;

namespace Dtat.Security.Jwt
{
	public static class Utility
	{
		static Utility()
		{
		}

		public static bool SetUserTokenByUser<T>(IUser<T> user, ISetting setting)
		{
			Validate(user: user, setting: setting);

			// **************************************************
			byte[] key =
				System.Text.Encoding.ASCII.GetBytes(setting.TokenSecretKey);

			var symmetricSecurityKey =
				new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key);

			var securityAlgorithm =
				Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature;

			var signingCredentials =
				new Microsoft.IdentityModel.Tokens
				.SigningCredentials(key: symmetricSecurityKey, algorithm: securityAlgorithm);
			// **************************************************

			// **************************************************
			var claims =
				GetClaimsByUser(user: user);

			var tokenDescriptor =
				new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
				{
					Issuer = setting.TokenIssuer,
					Audience = setting.TokenAudience,

					SigningCredentials = signingCredentials,

					Subject =
						new System.Security.Claims.ClaimsIdentity(claims: claims),

					Expires =
						System.DateTime.UtcNow.AddMinutes(setting.TokenExpiresInMinutes),
				};
			// **************************************************

			// **************************************************
			var tokenHandler =
				new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

			var securityToken =
				tokenHandler.CreateToken(tokenDescriptor: tokenDescriptor);

			string token =
				tokenHandler.WriteToken(token: securityToken);

			user.Token = token;
			// **************************************************

			return true;
		}

		private static
			System.Collections.Generic.IList<System.Security.Claims.Claim> GetClaimsByUser<T>(IUser<T> user)
		{
			var claims =
				new System.Collections.Generic.List<System.Security.Claims.Claim>();

			ValidateType<T>();

			// **************************************************
			if (user.Id != null)
			{
				var claim =
					new System.Security.Claims.Claim
					(type: nameof(user.Id), value: user.Id.ToString());

				claims.Add(claim);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(user.IP) == false)
			{
				var claim =
					new System.Security.Claims.Claim
					(type: nameof(user.IP), value: user.IP);

				claims.Add(claim);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(user.FirstName) == false)
			{
				var claim =
					new System.Security.Claims.Claim
					(type: nameof(user.FirstName), value: user.FirstName);

				claims.Add(claim);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(user.LastName) == false)
			{
				var claim =
					new System.Security.Claims.Claim
					(type: nameof(user.LastName), value: user.LastName);

				claims.Add(claim);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(user.Username) == false)
			{
				var claim =
					new System.Security.Claims.Claim
					(type: nameof(user.Username), value: user.Username);

				claims.Add(claim);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(user.EmailAddress) == false)
			{
				var claim =
					new System.Security.Claims.Claim
					(type: nameof(user.EmailAddress), value: user.EmailAddress);

				claims.Add(claim);
			}
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(user.CellPhoneNumber) == false)
			{
				var claim =
					new System.Security.Claims.Claim
					(type: nameof(user.CellPhoneNumber), value: user.CellPhoneNumber);

				claims.Add(claim);
			}
			// **************************************************

			// **************************************************
			if (user.Claims != null)
			{
				foreach (var claim in user.Claims)
				{
					if (string.IsNullOrWhiteSpace(claim.Value) == false)
					{
						var foundedClaim =
							claims
							.Where(current => current.Type.ToLower() == claim.Type.ToLower())
							.FirstOrDefault();

						if (foundedClaim == null)
						{
							claims.Add(claim);
						}
					}
				}
			}
			// **************************************************	

			return claims;
		}

		public static bool SetUserByUserToken<T>(IUser<T> user, ISetting setting)
		{
			Validate(user: user, setting: setting);

			// **************************************************
			var key =
				System.Text.Encoding.ASCII.GetBytes(setting.TokenSecretKey);

			var symmetricSecurityKey =
				new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(key: key);
			// **************************************************

			// **************************************************
			var tokenValidationParameters =
				new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = setting.TokenIssuer,
					ValidAudience = setting.TokenAudience,

					IssuerSigningKey = symmetricSecurityKey,

					// Set clockskew to zero so tokens expire
					// exactly at token expiration time (instead of 5 minutes later)
					ClockSkew = System.TimeSpan.Zero,
				};
			// **************************************************

			// **************************************************
			var tokenHandler =
				new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

			tokenHandler.ValidateToken(token: user.Token,
				validationParameters: tokenValidationParameters,
				out Microsoft.IdentityModel.Tokens.SecurityToken validatedToken);

			if (validatedToken is not System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtSecurityToken)
			{
				throw new System.Exception(message: "Invalid Token");
			}
			// **************************************************

			// **************************************************
			SetUserByToken(user: user, jwtSecurityToken: jwtSecurityToken);
			// **************************************************

			return true;
		}

		private static void SetUserByToken<T>
			(IUser<T> user, System.IdentityModel.Tokens.Jwt.JwtSecurityToken jwtSecurityToken)
		{
			var claims =
				jwtSecurityToken.Claims.ToList();

			System.Security.Claims.Claim claim = null;

			// **************************************************
			claim =
				claims
				.Where(current => current.Type.ToLower() == nameof(IUser<T>.Id).ToLower())
				.FirstOrDefault();

			if (claim != null)
			{
				ValidateType<T>();

				// Note: Below Code Does Not Work!
				//user.Id =
				//	(T)System.Convert.ChangeType(value: claim.Value, conversionType: typeof(T));

				user.Id =
					(T)System.ComponentModel.TypeDescriptor
					.GetConverter(typeof(T)).ConvertFromInvariantString(claim.Value);

				claims.Remove(claim);
			}
			// **************************************************

			// **************************************************
			claim =
				claims
				.Where(current => current.Type.ToLower() == nameof(IUser<T>.IP).ToLower())
				.FirstOrDefault();

			if (claim != null)
			{
				user.IP = claim.Value;

				claims.Remove(claim);
			}
			// **************************************************

			// **************************************************
			claim =
				claims
				.Where(current => current.Type.ToLower() == nameof(IUser<T>.FirstName).ToLower())
				.FirstOrDefault();

			if (claim != null)
			{
				user.FirstName = claim.Value;

				claims.Remove(claim);
			}
			// **************************************************

			// **************************************************
			claim =
				claims
				.Where(current => current.Type.ToLower() == nameof(IUser<T>.LastName).ToLower())
				.FirstOrDefault();

			if (claim != null)
			{
				user.LastName = claim.Value;

				claims.Remove(claim);
			}
			// **************************************************

			// **************************************************
			claim =
				claims
				.Where(current => current.Type.ToLower() == nameof(IUser<T>.Username).ToLower())
				.FirstOrDefault();

			if (claim != null)
			{
				user.Username = claim.Value;

				claims.Remove(claim);
			}
			// **************************************************

			// **************************************************
			claim =
				claims
				.Where(current => current.Type.ToLower() == nameof(IUser<T>.EmailAddress).ToLower())
				.FirstOrDefault();

			if (claim != null)
			{
				user.EmailAddress = claim.Value;

				claims.Remove(claim);
			}
			// **************************************************

			// **************************************************
			claim =
				claims
				.Where(current => current.Type.ToLower() == nameof(IUser<T>.CellPhoneNumber).ToLower())
				.FirstOrDefault();

			if (claim != null)
			{
				user.CellPhoneNumber = claim.Value;

				claims.Remove(claim);
			}
			// **************************************************

			// **************************************************
			if (user.Claims != null)
			{
				foreach (var currentClaim in claims)
				{
					switch (currentClaim.Type)
					{
						case "aud":
						case "exp":
						case "iat":
						case "iss":
						case "nbf":
						{
							break;
						}

						default:
						{
							user.AddClaim(currentClaim);
							break;
						}
					}
				}
			}
			// **************************************************
		}

		private static void ValidateType<T>()
		{
			switch (typeof(T).FullName)
			{
				case nameof(System) + "." + nameof(System.Guid):
				case nameof(System) + "." + nameof(System.Int32):
				case nameof(System) + "." + nameof(System.Int64):
				{
					break;
				}

				default:
				{
					throw new System.Exception
						(message: $"Type (T) should be ({nameof(System.Guid)}) or ({nameof(System.Int32)}) or ({nameof(System.Int64)})!");
				}
			}
		}

		private static void Validate<T>(IUser<T> user, ISetting setting)
		{
			if (user is null)
			{
				throw new System.ArgumentNullException
					(paramName: nameof(user),
					message: $"{nameof(user)} can not be null!");
			}

			if (setting == null)
			{
				throw new System.ArgumentNullException
					(paramName: nameof(setting),
					message: $"{nameof(setting)} can not be null!");
			}

			if (setting.TokenSecretKey == null)
			{
				throw new System.ArgumentNullException
					(paramName: nameof(setting.TokenSecretKey),
					message: $"{nameof(setting.TokenSecretKey)} can not be null!");
			}

			if (setting.TokenSecretKey == "")
			{
				throw new System.ArgumentNullException
					(paramName: nameof(setting.TokenSecretKey),
					message: $"{nameof(setting.TokenSecretKey)} can not be null string!");
			}

			setting.TokenSecretKey = setting.TokenSecretKey.Trim();

			if (setting.TokenSecretKey == "")
			{
				throw new System.ArgumentNullException
					(paramName: nameof(setting.TokenSecretKey),
					message: $"{nameof(setting.TokenSecretKey)} can not contains just space!");
			}

			if (setting.TokenSecretKey.Length < 16)
			{
				throw new System.ArgumentException
					(paramName: nameof(setting.TokenSecretKey),
					message: $"{nameof(setting.TokenSecretKey)} length should be greater than or equal to 16 characters!");
			}

			if (setting.TokenExpiresInMinutes < 1)
			{
				throw new System.ArgumentOutOfRangeException
					(paramName: nameof(setting.TokenExpiresInMinutes),
					message: $"{nameof(setting.TokenExpiresInMinutes)} should be greater than or equal to 1!");
			}
		}
	}
}
