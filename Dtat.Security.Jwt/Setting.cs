namespace Dtat.Security.Jwt
{
	public class Setting : object, ISetting
	{
		public Setting() : base()
		{
			TokenExpiresInMinutes = 20;
			TokenIssuer = "DTAT Security Center";
		}

		public string TokenIssuer { get; set; }

		public string TokenAudience { get; set; }

		/// <summary>
		/// Note: Should be minimum 16 characters!
		/// The encryption algorithm 'System.String' requires a key size of at least 'System.Int32' bits.
		/// </summary>
		public string TokenSecretKey { get; set; }

		public int TokenExpiresInMinutes { get; set; }
	}
}
