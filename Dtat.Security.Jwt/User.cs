using System.Linq;

namespace Dtat.Security.Jwt
{
	public class User : object, IUser<System.Guid>
	{
		public User() : base()
		{
			_claims =
				new System.Collections.Generic.List<System.Security.Claims.Claim>();
		}

		public System.Guid Id { get; set; }



		public string IP { get; set; }



		public string Token { get; set; }



		public string LastName { get; set; }

		public string FirstName { get; set; }



		public string Username { get; set; }

		public string EmailAddress { get; set; }

		public string CellPhoneNumber { get; set; }



		private readonly System.Collections.Generic.List<System.Security.Claims.Claim> _claims;

		public System.Collections.Generic.IReadOnlyList<System.Security.Claims.Claim> Claims
		{
			get
			{
				return _claims;
			}
		}

		public bool AddClaim(string type, string value)
		{
			if (string.IsNullOrWhiteSpace(type))
			{
				return false;
			}

			if (string.IsNullOrWhiteSpace(value))
			{
				return false;
			}

			var claim =
				new System.Security.Claims.Claim(type: type, value: value);

			return AddClaim(claim: claim);
		}

		public bool AddClaim(System.Security.Claims.Claim claim)
		{
			if (claim == null)
			{
				return false;
			}

			var founded =
				HasClaim(type: claim.Type);

			if (founded)
			{
				return false;
			}
			else
			{
				_claims.Add(claim);

				return true;
			}
		}

		public bool HasClaim(System.Security.Claims.Claim claim)
		{
			if (claim == null)
			{
				return false;
			}

			return HasClaim(type: claim.Type);
		}

		public bool HasClaim(string type)
		{
			if (string.IsNullOrWhiteSpace(type))
			{
				return false;
			}

			var foundedClaim =
				_claims
				.Where(current => current.Type.ToLower() == type.ToLower())
				.FirstOrDefault();

			if (foundedClaim == null)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}
}
