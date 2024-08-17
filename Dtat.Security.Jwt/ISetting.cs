namespace Dtat.Security.Jwt
{
	public interface ISetting
	{
		string TokenIssuer { get; set; }

		string TokenAudience { get; set; }

		/// <summary>
		/// Note: Should be minimum 16 characters!
		/// The encryption algorithm 'System.String' requires a key size of at least 'System.Int32' bits.
		/// </summary>
		string TokenSecretKey { get; set; }

		int TokenExpiresInMinutes { get; set; }
	}
}
