namespace Dtat.Security.Jwt
{
	public interface IUser<T>
	{
		T Id { get; set; }



		string IP { get; set; }



		public string Token { get; set; }



		string LastName { get; set; }

		string FirstName { get; set; }



		string Username { get; set; }

		string EmailAddress { get; set; }

		string CellPhoneNumber { get; set; }



		System.Collections.Generic.IReadOnlyList<System.Security.Claims.Claim> Claims { get; }

		bool AddClaim(string type, string value);

		bool AddClaim(System.Security.Claims.Claim claim);

		bool HasClaim(string type);

		bool HasClaim(System.Security.Claims.Claim claim);
	}
}
