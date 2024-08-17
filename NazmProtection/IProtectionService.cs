
namespace NazmProtection
{
	public interface IProtectionService
	{
		string Encrypt(string plainText);
		string Decrypt(string cipherText);
	}
}
